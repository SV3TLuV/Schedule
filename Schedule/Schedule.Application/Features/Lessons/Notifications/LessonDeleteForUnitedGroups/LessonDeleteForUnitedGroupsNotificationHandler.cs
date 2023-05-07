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
        var lesson = notification.Lesson;
        
        var unitedGroupIds = lesson.Timetable.Group.GroupGroups
            .Select(e => e.GroupId2);

        await _context.Set<Lesson>()
            .Include(e => e.Timetable)
            .AsNoTrackingWithIdentityResolution()
            .Where(e => 
                e.Number == lesson.Number &&
                unitedGroupIds.Contains(e.Timetable.GroupId) &&
                e.Timetable.DateId == lesson.Timetable.DateId)
            .ExecuteDeleteAsync(cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}