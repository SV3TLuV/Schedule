using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.LessonTemplates.Notifications.LessonTemplateDeleteForUnitedGroups;

public sealed class LessonTemplateDeleteForUnitedGroupsNotificationHandler
    : INotificationHandler<LessonTemplateDeleteForUnitedGroupsNotification>
{
    private readonly IScheduleDbContext _context;

    public LessonTemplateDeleteForUnitedGroupsNotificationHandler(
        IScheduleDbContext context)
    {
        _context = context;
    }

    public async Task Handle(LessonTemplateDeleteForUnitedGroupsNotification notification,
        CancellationToken cancellationToken)
    {
        var unitedGroupIds = notification.LessonTemplate.Template.Group.GroupGroups
            .Select(e => e.GroupId2)
            .ToArray();
        
        var lessonTemplates = await _context.Set<LessonTemplate>()
            .Include(e => e.Template)
            .Where(e =>
                e.Number == notification.LessonTemplate.Number &&
                unitedGroupIds.Contains(e.Template.GroupId) &&
                e.Template.TermId == notification.LessonTemplate.Template.TermId)
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync(cancellationToken);
        
        await _context.Set<LessonTemplateTeacherClassroom>()
            .Where(e => lessonTemplates
                .Select(l => l.LessonTemplateId)
                .Contains(e.LessonTemplateId))
            .ExecuteDeleteAsync(cancellationToken);
        
        _context.Set<LessonTemplate>().RemoveRange(lessonTemplates);
        await _context.SaveChangesAsync(cancellationToken);
    }
}