using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class DisciplineNameRepository(IScheduleDbContext context) : Repository(context), IDisciplineNameRepository
{
    public async Task<int> AddIfNotExistAsync(string name, CancellationToken cancellationToken = default)
    {
        int id;
        var disciplineNameDb = await Context.DisciplineNames.FirstOrDefaultAsync(e =>
            e.Name == name, cancellationToken);

        if (disciplineNameDb is null)
        {
            var created = await Context.DisciplineNames.AddAsync(new DisciplineName
            {
                Name = name
            }, cancellationToken);

            await Context.SaveChangesAsync(cancellationToken);

            id = created.Entity.DisciplineNameId;
        } else if (disciplineNameDb.IsDeleted)
        {
            disciplineNameDb.IsDeleted = false;
            Context.DisciplineNames.Update(disciplineNameDb);

            await Context.SaveChangesAsync(cancellationToken);

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
        var disciplineNameDb = await Context.DisciplineNames.FirstOrDefaultAsync(e =>
            e.DisciplineNameId == disciplineName.DisciplineNameId, cancellationToken);

        if (disciplineNameDb is null)
        {
            throw new NotFoundException(nameof(DisciplineName), disciplineName.DisciplineNameId);
        }

        var searchByName = await Context.DisciplineNames.FirstOrDefaultAsync(e =>
            e.Name == disciplineName.Name, cancellationToken);

        if (searchByName is not null)
        {
            throw new AlreadyExistsException(disciplineName.Name);
        }

        disciplineNameDb.Name = disciplineName.Name;

        Context.DisciplineNames.Update(disciplineNameDb);

        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var disciplineNameDb = await Context.DisciplineNames.FirstOrDefaultAsync(e =>
            e.DisciplineNameId == id, cancellationToken);

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
        var disciplineNameDb = await Context.DisciplineNames.FirstOrDefaultAsync(e =>
            e.DisciplineNameId == id, cancellationToken);

        if (disciplineNameDb is null)
        {
            throw new NotFoundException(nameof(DisciplineName), id);
        }

        disciplineNameDb.IsDeleted = false;

        Context.DisciplineNames.Update(disciplineNameDb);
        await Context.SaveChangesAsync(cancellationToken);
    }
}