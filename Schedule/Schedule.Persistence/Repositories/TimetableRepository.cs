using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class TimetableRepository(
    IScheduleDbContext context,
    ILessonRepository lessonRepository,
    IDateInfoService dateInfoService) : ITimetableRepository
{
    public async Task<int> CreateAsync(Timetable timetable, CancellationToken cancellationToken = default)
    {
        timetable.Created = dateInfoService.CurrentDate;

        var created = await context.Timetables.AddAsync(timetable, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return created.Entity.TimetableId;
    }

    public async Task UpdateAsync(Timetable timetable, CancellationToken cancellationToken = default)
    {
        var timetableDb = await context.Timetables.FirstOrDefaultAsync(e =>
            e.TimetableId == timetable.TimetableId, cancellationToken);

        if (timetableDb is null)
        {
            throw new NotFoundException(nameof(Timetable), timetable.TimetableId);
        }

        timetableDb.Ended = timetable.Ended;

        context.Timetables.Update(timetableDb);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task CreateForGroupAsync(int groupId, CancellationToken cancellationToken = default)
    {
        const int pairCount = 8;

        await context.WithTransactionAsync(async () =>
        {
            var group = await context.Groups
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.GroupId == groupId, cancellationToken);

            if (group is null)
            {
                throw new NotFoundException(nameof(Group), groupId);
            }

            var dayIds = await context.Days
                .AsNoTracking()
                .Select(e => e.DayId)
                .ToListAsync(cancellationToken);

            var weekTypeIds = await context.WeekTypes
                .AsNoTracking()
                .Select(e => e.WeekTypeId)
                .ToListAsync(cancellationToken);

            foreach (var weekTypeId in weekTypeIds)
            {
                foreach (var dayId in dayIds)
                {
                    var timetableId = await CreateAsync(new Timetable
                    {
                        GroupId = group.GroupId,
                        DayId = dayId,
                        WeekTypeId = weekTypeId,
                    }, cancellationToken);

                    for (var i = 1; i <= pairCount; i++)
                    {
                        await lessonRepository.CreateAsync(new Lesson
                        {
                            Number = i,
                            TimetableId = timetableId,
                        }, cancellationToken);
                    }
                }
            }

            await context.SaveChangesAsync(cancellationToken);
        }, cancellationToken);
    }
}