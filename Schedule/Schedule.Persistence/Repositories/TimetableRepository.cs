using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class TimetableRepository(
    IScheduleDbContext context,
    IDateInfoService dateInfoService) : Repository(context), ITimetableRepository
{
    public async Task<int> CreateAsync(Timetable timetable, CancellationToken cancellationToken = default)
    {
        timetable.Created = dateInfoService.CurrentDate;

        var created = await Context.Timetables.AddAsync(timetable, cancellationToken);

        await Context.SaveChangesAsync(cancellationToken);

        return created.Entity.TimetableId;
    }

    public async Task UpdateAsync(Timetable timetable, CancellationToken cancellationToken = default)
    {
        var timetableDb = await Context.Timetables.FirstOrDefaultAsync(e =>
            e.TimetableId == timetable.TimetableId, cancellationToken);

        if (timetableDb is null)
        {
            throw new NotFoundException(nameof(Timetable), timetable.TimetableId);
        }

        timetableDb.Ended = timetable.Ended;

        Context.Timetables.Update(timetableDb);
        await Context.SaveChangesAsync(cancellationToken);
    }
}