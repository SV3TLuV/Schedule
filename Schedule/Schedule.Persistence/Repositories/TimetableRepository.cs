using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class TimetableRepository : Repository, ITimetableRepository
{
    private readonly ILessonRepository _lessonRepository;
    private readonly IDateInfoService _dateInfoService;

    public TimetableRepository( IScheduleDbContext context,
        ILessonRepository lessonRepository,
        IDateInfoService dateInfoService) : base(context)
    {
        lessonRepository.UseContext(context);
        _lessonRepository = lessonRepository;
        _dateInfoService = dateInfoService;
    }

    public async Task<int> CreateAsync(Timetable timetable, CancellationToken cancellationToken = default)
    {
        timetable.Created = _dateInfoService.CurrentDate;

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

    public async Task CreateForGroupAsync(int groupId, CancellationToken cancellationToken = default)
    {
        const int pairCount = 8;

        await Context.WithTransactionAsync(async () =>
        {
            var group = await Context.Groups
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.GroupId == groupId, cancellationToken);

            if (group is null)
            {
                throw new NotFoundException(nameof(Group), groupId);
            }

            var dayIds = await Context.Days
                .AsNoTracking()
                .Select(e => e.DayId)
                .ToListAsync(cancellationToken);

            var weekTypeIds = await Context.WeekTypes
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
                        await _lessonRepository.CreateAsync(new Lesson
                        {
                            Number = i,
                            TimetableId = timetableId,
                        }, cancellationToken);
                    }
                }
            }

            await Context.SaveChangesAsync(cancellationToken);
        }, cancellationToken);
    }
}