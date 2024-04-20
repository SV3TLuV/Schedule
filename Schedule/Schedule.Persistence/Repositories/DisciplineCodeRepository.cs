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
        var disciplineCodeDb = await Context.DisciplineCodes
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Code == code, cancellationToken);

        if (disciplineCodeDb is not null)
        {
            return;
        }

        await Context.DisciplineCodes.AddAsync(new DisciplineCode
        {
            Code = code
        }, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(DisciplineCode disciplineCode, CancellationToken cancellationToken = default)
    {
        await Context.WithTransactionAsync(async () =>
        {
            var disciplineCodeDb = await Context.DisciplineCodes
                .FirstOrDefaultAsync(e => e.DisciplineCodeId == disciplineCode.DisciplineCodeId, cancellationToken);

            if (disciplineCodeDb is null)
            {
                throw new NotFoundException(nameof(DisciplineCode), disciplineCode.DisciplineCodeId);
            }

            disciplineCodeDb.DisciplineCodeId = disciplineCode.DisciplineCodeId;
            disciplineCodeDb.Code = disciplineCode.Code;
            disciplineCodeDb.IsDeleted = disciplineCode.IsDeleted;

            Context.DisciplineCodes.Update(disciplineCodeDb);
            await Context.SaveChangesAsync(cancellationToken);
        }, cancellationToken);
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