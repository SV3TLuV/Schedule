using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class SpecialityRepository(IScheduleDbContext context) : Repository(context), ISpecialityRepository
{
    public async Task<int> CreateAsync(Speciality speciality, CancellationToken cancellationToken = default)
    {
        int id;
        var specialityDb = await Context.Specialities.FirstOrDefaultAsync(e =>
            e.Name == speciality.Name, cancellationToken);

        if (specialityDb is null)
        {
            var created = await Context.Specialities.AddAsync(speciality, cancellationToken);

            await Context.SaveChangesAsync(cancellationToken);

            id = created.Entity.SpecialityId;
        } else if (specialityDb.IsDeleted)
        {
            specialityDb.IsDeleted = false;
            Context.Specialities.Update(specialityDb);

            await Context.SaveChangesAsync(cancellationToken);

            id = specialityDb.SpecialityId;
        }
        else
        {
            throw new AlreadyExistsException(speciality.Name);
        }

        return id;
    }

    public async Task UpdateAsync(Speciality speciality, CancellationToken cancellationToken = default)
    {
        var specialityDb = await Context.Specialities.FirstOrDefaultAsync(e =>
            e.SpecialityId == speciality.SpecialityId, cancellationToken);

        if (specialityDb is null)
        {
            throw new NotFoundException(nameof(Speciality), speciality.SpecialityId);
        }

        var searchByName = await Context.Specialities.FirstOrDefaultAsync(e =>
            e.Name == speciality.Name, cancellationToken);

        if (searchByName is not null)
        {
            throw new AlreadyExistsException(searchByName.Name);
        }

        specialityDb.SpecialityId = speciality.SpecialityId;
        specialityDb.Code = speciality.Code;
        specialityDb.Name = speciality.Name;
        specialityDb.MaxTermId = speciality.MaxTermId;
        specialityDb.IsDeleted = speciality.IsDeleted;

        Context.Specialities.Update(specialityDb);

        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var specialityDb = await Context.Specialities.FirstOrDefaultAsync(e =>
            e.SpecialityId == id, cancellationToken);

        if (specialityDb is null)
        {
            throw new NotFoundException(nameof(Speciality), id);
        }

        specialityDb.IsDeleted = true;

        Context.Specialities.Update(specialityDb);

        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task RestoreAsync(int id, CancellationToken cancellationToken)
    {
        var specialityDb = await Context.Specialities.FirstOrDefaultAsync(e =>
            e.SpecialityId == id, cancellationToken);

        if (specialityDb is null)
        {
            throw new NotFoundException(nameof(Speciality), id);
        }

        specialityDb.IsDeleted = false;

        Context.Specialities.Update(specialityDb);

        await Context.SaveChangesAsync(cancellationToken);
    }
}