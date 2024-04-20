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

    public async Task UpdateEndedAsync(Timetable timetable, CancellationToken cancellationToken = default)
    {
        timetable.Ended = dateInfoService.CurrentDate;

        Context.Timetables.Update(timetable);

        await Context.SaveChangesAsync(cancellationToken);
    }
}