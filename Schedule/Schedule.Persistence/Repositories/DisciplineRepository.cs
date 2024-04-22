using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class DisciplineRepository(IScheduleDbContext context) : Repository(context), IDisciplineRepository
{
    public async Task<int> CreateAsync(Discipline discipline, CancellationToken cancellationToken)
    {
        int id;
        var disciplineDb = await Context.Disciplines.FirstOrDefaultAsync(e =>
            e.TermId == discipline.TermId &&
            e.NameId == discipline.NameId &&
            e.CodeId == discipline.CodeId &&
            e.SpecialityId == discipline.SpecialityId, cancellationToken);

        if (disciplineDb is null)
        {
            var created = await Context.Disciplines.AddAsync(discipline, cancellationToken);

            await Context.SaveChangesAsync(cancellationToken);

            id = created.Entity.DisciplineId;
        }
        else if (disciplineDb.IsDeleted)
        {
            disciplineDb.IsDeleted = false;
            Context.Disciplines.Update(disciplineDb);

            await Context.SaveChangesAsync(cancellationToken);

            id = disciplineDb.DisciplineId;
        }
        else
        {
            throw new AlreadyExistsException(discipline.Name.Name);
        }

        return id;
    }

    public async Task UpdateAsync(Discipline discipline, CancellationToken cancellationToken)
    {
        var disciplineDb = await Context.Disciplines
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.DisciplineId == discipline.DisciplineId, cancellationToken);

        if (disciplineDb is null)
        {
            throw new NotFoundException(nameof(Discipline), discipline.DisciplineId);
        }

        var searchDiscipline = await Context.Disciplines
            .AsNoTracking()
            .Include(e => e.Name)
            .FirstOrDefaultAsync(e =>
                e.CodeId == discipline.CodeId &&
                e.NameId == discipline.NameId &&
                e.SpecialityId == discipline.SpecialityId &&
                e.TermId == discipline.TermId &&
                e.DisciplineId != disciplineDb.DisciplineId, cancellationToken);

        if (searchDiscipline is not null)
        {
            throw new AlreadyExistsException(searchDiscipline.Name.Name);
        }

        disciplineDb.NameId = discipline.NameId;
        disciplineDb.CodeId = discipline.CodeId;
        disciplineDb.TotalHours = discipline.TotalHours;
        disciplineDb.TermId = discipline.TermId;
        disciplineDb.SpecialityId = discipline.SpecialityId;
        disciplineDb.TypeId = discipline.TypeId;

        Context.Disciplines.Update(disciplineDb);

        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var disciplineDb = await Context.Disciplines.FirstOrDefaultAsync(e =>
            e.DisciplineId == id, cancellationToken);

        if (disciplineDb is null)
        {
            throw new NotFoundException(nameof(Discipline), id);
        }

        disciplineDb.IsDeleted = true;

        Context.Disciplines.Update(disciplineDb);

        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task RestoreAsync(int id, CancellationToken cancellationToken)
    {
        var disciplineDb = await Context.Disciplines.FirstOrDefaultAsync(e =>
            e.DisciplineId == id, cancellationToken);

        if (disciplineDb is null)
        {
            throw new NotFoundException(nameof(Discipline), id);
        }

        disciplineDb.IsDeleted = false;

        Context.Disciplines.Update(disciplineDb);

        await Context.SaveChangesAsync(cancellationToken);
    }
}