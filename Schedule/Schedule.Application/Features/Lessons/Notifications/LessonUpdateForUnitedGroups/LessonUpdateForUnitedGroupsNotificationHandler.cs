using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Lessons.Notifications.LessonUpdateForUnitedGroups;

public sealed class LessonUpdateForUnitedGroupsNotificationHandler
    : INotificationHandler<LessonUpdateForUnitedGroupsNotification>
{
    private readonly IScheduleDbContext _context;

    public LessonUpdateForUnitedGroupsNotificationHandler(
        IScheduleDbContext context)
    {
        _context = context;
    }

    public async Task Handle(LessonUpdateForUnitedGroupsNotification notification,
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

        var lessons = await _context.Set<Lesson>()
            .Include(e => e.Timetable)
            .AsNoTrackingWithIdentityResolution()
            .Where(e =>
                e.Number == lesson.Number &&
                unitedGroupIds.Contains(e.Timetable.GroupId) &&
                e.Timetable.DateId == lesson.Timetable.DateId)
            .ToListAsync(cancellationToken);

        foreach (var lessonToUpdate in lessons)
        {
            lessonToUpdate.TimeId = lesson.TimeId;
            lessonToUpdate.Subgroup = lesson.Subgroup;
            lessonToUpdate.IsChanged = lesson.IsChanged;
            lessonToUpdate.DisciplineId = lesson.DisciplineId;
            lessonToUpdate.LessonTeacherClassrooms = lesson.LessonTeacherClassrooms;
            _context.Set<Lesson>().Update(lessonToUpdate);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}