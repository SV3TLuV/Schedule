using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class SpecialityRepository(IScheduleDbContext context) : ISpecialityRepository
{
    public async Task<int> CreateAsync(Speciality speciality, CancellationToken cancellationToken = default)
    {
        int id;
        var specialityDb = await context.Specialities.FirstOrDefaultAsync(e =>
            e.Name == speciality.Name &&
            e.Code == speciality.Code, cancellationToken);

        if (specialityDb is null)
        {
            var created = await context.Specialities.AddAsync(speciality, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);

            id = created.Entity.SpecialityId;
        } else if (specialityDb.IsDeleted)
        {
            specialityDb.IsDeleted = false;
            context.Specialities.Update(specialityDb);

            await context.SaveChangesAsync(cancellationToken);

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
        var specialityDb = await context.Specialities.FirstOrDefaultAsync(e =>
            e.SpecialityId == speciality.SpecialityId, cancellationToken);

        if (specialityDb is null)
        {
            throw new NotFoundException(nameof(Speciality), speciality.SpecialityId);
        }

        var searchByName = await context.Specialities.FirstOrDefaultAsync(e =>
            e.Name == speciality.Name &&
            e.Code == speciality.Code &&
            e.SpecialityId != specialityDb.SpecialityId, cancellationToken);

        if (searchByName is not null)
        {
            throw new AlreadyExistsException(searchByName.Name);
        }

        specialityDb.Code = speciality.Code;
        specialityDb.Name = speciality.Name;
        specialityDb.MaxTermId = speciality.MaxTermId;

        context.Specialities.Update(specialityDb);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var specialityDb = await context.Specialities.FirstOrDefaultAsync(e =>
            e.SpecialityId == id, cancellationToken);

        if (specialityDb is null)
        {
            throw new NotFoundException(nameof(Speciality), id);
        }

        specialityDb.IsDeleted = true;

        context.Specialities.Update(specialityDb);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task RestoreAsync(int id, CancellationToken cancellationToken)
    {
        var specialityDb = await context.Specialities.FirstOrDefaultAsync(e =>
            e.SpecialityId == id, cancellationToken);

        if (specialityDb is null)
        {
            throw new NotFoundException(nameof(Speciality), id);
        }

        specialityDb.IsDeleted = false;

        context.Specialities.Update(specialityDb);

        await context.SaveChangesAsync(cancellationToken);
    }
}