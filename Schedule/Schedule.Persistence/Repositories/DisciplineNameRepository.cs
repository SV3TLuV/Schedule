using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class DisciplineNameRepository(IScheduleDbContext context) : Repository(context), IDisciplineNameRepository
{
    public async Task AddIfNotExistAsync(string name, CancellationToken cancellationToken = default)
    {
        var disciplineNameDb = await Context.DisciplineNames
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Name == name, cancellationToken);

        if (disciplineNameDb is not null)
        {
            return;
        }

        await Context.DisciplineNames.AddAsync(new DisciplineName
        {
            Name = name
        }, cancellationToken);

        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(DisciplineName disciplineName, CancellationToken cancellationToken = default)
    {
        var disciplineNameDb = await Context.DisciplineNames
            .FirstOrDefaultAsync(e => e.DisciplineNameId == disciplineName.DisciplineNameId, cancellationToken);

        if (disciplineNameDb is null)
        {
            throw new NotFoundException(nameof(DisciplineName), disciplineName.DisciplineNameId);
        }

        disciplineNameDb.DisciplineNameId = disciplineName.DisciplineNameId;
        disciplineNameDb.Name = disciplineName.Name;
        disciplineNameDb.IsDeleted = disciplineName.IsDeleted;

        Context.DisciplineNames.Update(disciplineNameDb);

        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var disciplineNameDb = await Context.DisciplineNames
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.DisciplineNameId == id, cancellationToken);

        if (disciplineNameDb is null)
        {
            throw new NotFoundException(nameof(DisciplineName), id);
        }

        disciplineNameDb.IsDeleted = true;

        Context.DisciplineNames.Update(disciplineNameDb);
        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task RestoreAsync(int id, CancellationToken cancellationToken = default)
    {
        var disciplineNameDb = await Context.DisciplineNames
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.DisciplineNameId == id, cancellationToken);

        if (disciplineNameDb is null)
        {
            throw new NotFoundException(nameof(DisciplineName), id);
        }

        disciplineNameDb.IsDeleted = false;

        Context.DisciplineNames.Update(disciplineNameDb);
        await Context.SaveChangesAsync(cancellationToken);
    }
}