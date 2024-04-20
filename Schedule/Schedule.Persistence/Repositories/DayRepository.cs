using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class DayRepository(IScheduleDbContext context) : Repository(context), IDayRepository
{
    public async Task UpdateAsync(Day day, CancellationToken cancellationToken = default)
    {
        var dayDb = await Context.Days.FirstOrDefaultAsync(e => e.DayId == day.DayId, cancellationToken);

        if (dayDb is null)
        {
            throw new NotFoundException(nameof(Day), day.DayId);
        }

        dayDb.IsStudy = day.IsStudy;

        Context.Days.Update(dayDb);

        await Context.SaveChangesAsync(cancellationToken);
    }
}