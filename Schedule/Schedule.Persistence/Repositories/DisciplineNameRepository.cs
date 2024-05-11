using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class DisciplineNameRepository(IScheduleDbContext context) : IDisciplineNameRepository
{
    public async Task<int> AddIfNotExistAsync(string name, CancellationToken cancellationToken = default)
    {
        int id;
        var disciplineNameDb = await context.DisciplineNames.FirstOrDefaultAsync(e =>
            e.Name == name, cancellationToken);

        if (disciplineNameDb is null)
        {
            var created = await context.DisciplineNames.AddAsync(new DisciplineName
            {
                Name = name
            }, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);

            id = created.Entity.DisciplineNameId;
        } else if (disciplineNameDb.IsDeleted)
        {
            disciplineNameDb.IsDeleted = false;
            context.DisciplineNames.Update(disciplineNameDb);

            await context.SaveChangesAsync(cancellationToken);

            id = disciplineNameDb.DisciplineNameId;
        }
        else
        {
            throw new AlreadyExistsException(name);
        }

        return id;
    }

    public async Task UpdateAsync(DisciplineName disciplineName, CancellationToken cancellationToken = default)
    {
        var disciplineNameDb = await context.DisciplineNames.FirstOrDefaultAsync(e =>
            e.DisciplineNameId == disciplineName.DisciplineNameId, cancellationToken);

        if (disciplineNameDb is null)
        {
            throw new NotFoundException(nameof(DisciplineName), disciplineName.DisciplineNameId);
        }

        var searchByName = await context.DisciplineNames.FirstOrDefaultAsync(e =>
            e.Name == disciplineName.Name, cancellationToken);

        if (searchByName is not null)
        {
            throw new AlreadyExistsException(disciplineName.Name);
        }

        disciplineNameDb.Name = disciplineName.Name;

        context.DisciplineNames.Update(disciplineNameDb);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var disciplineNameDb = await context.DisciplineNames.FirstOrDefaultAsync(e =>
            e.DisciplineNameId == id, cancellationToken);

        if (disciplineNameDb is null)
        {
            throw new NotFoundException(nameof(DisciplineName), id);
        }

        disciplineNameDb.IsDeleted = true;

        context.DisciplineNames.Update(disciplineNameDb);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task RestoreAsync(int id, CancellationToken cancellationToken = default)
    {
        var disciplineNameDb = await context.DisciplineNames.FirstOrDefaultAsync(e =>
            e.DisciplineNameId == id, cancellationToken);

        if (disciplineNameDb is null)
        {
            throw new NotFoundException(nameof(DisciplineName), id);
        }

        disciplineNameDb.IsDeleted = false;

        context.DisciplineNames.Update(disciplineNameDb);
        await context.SaveChangesAsync(cancellationToken);
    }
}