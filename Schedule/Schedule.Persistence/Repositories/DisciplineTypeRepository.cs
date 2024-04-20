using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class DisciplineTypeRepository(IScheduleDbContext context) : Repository(context), IDisciplineTypeRepository
{
    public async Task AddIfNotExistAsync(string name, CancellationToken cancellationToken = default)
    {
        var disciplineTypeDb = await Context.DisciplineTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Name == name, cancellationToken);

        if (disciplineTypeDb is not null)
        {
            return;
        }

        await Context.DisciplineTypes.AddAsync(new DisciplineType
        {
            Name = name
        }, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(DisciplineType disciplineType, CancellationToken cancellationToken = default)
    {
        await Context.WithTransactionAsync(async () =>
        {
            var disciplineTypeDb = await Context.DisciplineTypes
                .FirstOrDefaultAsync(e => e.DisciplineTypeId == disciplineType.DisciplineTypeId, cancellationToken);

            if (disciplineTypeDb is null)
            {
                throw new NotFoundException(nameof(DisciplineType), disciplineType.DisciplineTypeId);
            }

            disciplineTypeDb.DisciplineTypeId = disciplineType.DisciplineTypeId;
            disciplineTypeDb.Name = disciplineType.Name;

            Context.DisciplineTypes.Update(disciplineTypeDb);
            await Context.SaveChangesAsync(cancellationToken);
        }, cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var disciplineTypeDb = await Context.DisciplineTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.DisciplineTypeId == id, cancellationToken);

        if (disciplineTypeDb is null)
        {
            throw new NotFoundException(nameof(DisciplineType), id);
        }

        Context.DisciplineTypes.Remove(disciplineTypeDb);
        await Context.SaveChangesAsync(cancellationToken);
    }
}