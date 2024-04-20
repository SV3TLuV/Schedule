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
        var id = default(int);

        await Context.WithTransactionAsync(async () =>
        {
            var created = await Context.Specialities.AddAsync(speciality, cancellationToken);

            id = created.Entity.SpecialityId;

            await Context.SaveChangesAsync(cancellationToken);
        }, cancellationToken);

        return id;
    }

    public async Task UpdateAsync(Speciality speciality, CancellationToken cancellationToken = default)
    {
        await Context.WithTransactionAsync(async () =>
        {
            var specialityDb = await Context.Specialities
                .FirstOrDefaultAsync(e => e.SpecialityId == speciality.SpecialityId, cancellationToken);

            if (specialityDb is null)
            {
                throw new NotFoundException(nameof(Speciality), speciality.SpecialityId);
            }

            specialityDb.SpecialityId = speciality.SpecialityId;
            specialityDb.Code = speciality.Code;
            specialityDb.Name = speciality.Name;
            specialityDb.MaxTermId = speciality.MaxTermId;
            specialityDb.IsDeleted = speciality.IsDeleted;

            Context.Specialities.Update(specialityDb);
            await Context.SaveChangesAsync(cancellationToken);
        }, cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var specialityDb = await Context.Specialities
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.SpecialityId == id, cancellationToken);

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
        var specialityDb = await Context.Specialities
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.SpecialityId == id, cancellationToken);

        if (specialityDb is null)
        {
            throw new NotFoundException(nameof(Speciality), id);
        }

        specialityDb.IsDeleted = false;

        Context.Specialities.Update(specialityDb);
        await Context.SaveChangesAsync(cancellationToken);
    }
}