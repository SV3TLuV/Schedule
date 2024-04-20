using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class DisciplineCodeRepository(IScheduleDbContext context) : Repository(context), IDisciplineCodeRepository
{
    public async Task AddIfNotExistAsync(string code, CancellationToken cancellationToken = default)
    {
        var disciplineCodeDb = await Context.DisciplineCodes.FirstOrDefaultAsync(e =>
            e.Code == code, cancellationToken);

        if (disciplineCodeDb is null)
        {
            await Context.DisciplineCodes.AddAsync(new DisciplineCode
            {
                Code = code
            }, cancellationToken);
        }
        else if (disciplineCodeDb.IsDeleted)
        {
            disciplineCodeDb.IsDeleted = false;
            Context.DisciplineCodes.Update(disciplineCodeDb);
        }
        else
        {
            throw new AlreadyExistsException(code);
        }

        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(DisciplineCode disciplineCode, CancellationToken cancellationToken = default)
    {
        var disciplineCodeDb = await Context.DisciplineCodes.FirstOrDefaultAsync(e =>
            e.DisciplineCodeId == disciplineCode.DisciplineCodeId, cancellationToken);

        if (disciplineCodeDb is null)
        {
            throw new NotFoundException(nameof(DisciplineCode), disciplineCode.DisciplineCodeId);
        }

        var searchByCode = await Context.DisciplineCodes.FirstOrDefaultAsync(e =>
            e.Code == disciplineCode.Code, cancellationToken);

        if (searchByCode is not null)
        {
            throw new AlreadyExistsException(disciplineCode.Code);
        }

        disciplineCodeDb.Code = disciplineCode.Code;

        Context.DisciplineCodes.Update(disciplineCodeDb);

        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var disciplineCodeDb = await Context.DisciplineCodes
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.DisciplineCodeId == id, cancellationToken);

        if (disciplineCodeDb is null)
        {
            throw new NotFoundException(nameof(DisciplineCode), id);
        }

        disciplineCodeDb.IsDeleted = true;

        Context.DisciplineCodes.Update(disciplineCodeDb);

        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task RestoreAsync(int id, CancellationToken cancellationToken = default)
    {
        var disciplineCodeDb = await Context.DisciplineCodes
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.DisciplineCodeId == id, cancellationToken);

        if (disciplineCodeDb is null)
        {
            throw new NotFoundException(nameof(DisciplineCode), id);
        }

        disciplineCodeDb.IsDeleted = false;

        Context.DisciplineCodes.Update(disciplineCodeDb);

        await Context.SaveChangesAsync(cancellationToken);
    }
}