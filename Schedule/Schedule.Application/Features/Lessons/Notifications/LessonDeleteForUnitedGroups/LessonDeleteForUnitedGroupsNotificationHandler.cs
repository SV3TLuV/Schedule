using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Lessons.Notifications.LessonDeleteForUnitedGroups;

public sealed class LessonDeleteForUnitedGroupsNotificationHandler
    : INotificationHandler<LessonDeleteForUnitedGroupsNotification>
{
    private readonly IScheduleDbContext _context;

    public LessonDeleteForUnitedGroupsNotificationHandler(
        IScheduleDbContext context)
    {
        _context = context;
    }

    public async Task Handle(LessonDeleteForUnitedGroupsNotification notification,
        CancellationToken cancellationToken)
    {
        var unitedGroupIds = notification.Lesson.Timetable.Group.GroupGroups
            .Select(e => e.GroupId2)
            .ToArray();

        var lessons = await _context.Set<Lesson>()
            .Include(e => e.Timetable)
            .Where(e =>
                e.Number == notification.Lesson.Number &&
                unitedGroupIds.Contains(e.Timetable.GroupId) &&
                e.Timetable.DateId == notification.Lesson.Timetable.DateId)
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync(cancellationToken);

        await _context.Set<LessonTeacherClassroom>()
            .Where(e => lessons.Select(l => l.LessonId).Contains(e.LessonId))
            .ExecuteDeleteAsync(cancellationToken);
        
        _context.Set<Lesson>().RemoveRange(lessons);
        await _context.SaveChangesAsync(cancellationToken);
    }
}