using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class SurnameRepository(IScheduleDbContext context) : Repository(context), ISurnameRepository
{
    public async Task AddIfNotExistAsync(string surname, CancellationToken cancellationToken = default)
    {
        var nameDb = await Context.Surnames
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Value == surname, cancellationToken);

        if (nameDb is not null)
        {
            return;
        }

        await Context.Surnames.AddAsync(new Surname
        {
            Value = surname
        }, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
    }
}