using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class MiddleNameRepository(IScheduleDbContext context) : Repository(context), IMiddleNameRepository
{
    public async Task AddIfNotExistAsync(string middleName, CancellationToken cancellationToken = default)
    {
        var nameDb = await Context.MiddleNames
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Value == middleName, cancellationToken);

        if (nameDb is not null)
        {
            return;
        }

        await Context.MiddleNames.AddAsync(new MiddleName
        {
            Value = middleName
        }, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
    }
}