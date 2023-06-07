using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Lessons.Notifications.LessonCreateForUnitedGroups;

public sealed class LessonCreateForUnitedGroupsNotificationHandler
    : INotificationHandler<LessonCreateForUnitedGroupsNotification>
{
    private readonly IScheduleDbContext _context;

    public LessonCreateForUnitedGroupsNotificationHandler(
        IScheduleDbContext context)
    {
        _context = context;
    }

    public async Task Handle(LessonCreateForUnitedGroupsNotification notification,
        CancellationToken cancellationToken)
    {
        var lesson = await _context.Set<Lesson>()
            .Include(e => e.Timetable)
            .ThenInclude(e => e.Group)
            .ThenInclude(e => e.GroupGroups)
            .AsNoTrackingWithIdentityResolution()
            .AsSplitQuery()
            .FirstOrDefaultAsync(e => e.LessonId == notification.LessonId, cancellationToken);

        if (lesson is null)
            throw new NotFoundException(nameof(Lesson), notification.LessonId);

        var unitedGroupIds = lesson.Timetable.Group.GroupGroups.Select(e => e.GroupId2);

        var timetableIds = await _context.Set<Timetable>()
            .AsNoTrackingWithIdentityResolution()
            .Where(e =>
                unitedGroupIds.Contains(e.GroupId) &&
                e.DateId == lesson.Timetable.DateId)
            .Select(e => e.TimetableId)
            .ToListAsync(cancellationToken);

        foreach (var timetableId in timetableIds)
        {
            var newLesson = new Lesson
            {
                Number = lesson.Number,
                Subgroup = lesson.Subgroup,
                TimeId = lesson.TimeId,
                TimetableId = timetableId,
                DisciplineId = lesson.DisciplineId,
                IsChanged = lesson.IsChanged,
                LessonTeacherClassrooms = lesson.LessonTeacherClassrooms
            };
            await _context.Set<Lesson>().AddAsync(newLesson, cancellationToken);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}