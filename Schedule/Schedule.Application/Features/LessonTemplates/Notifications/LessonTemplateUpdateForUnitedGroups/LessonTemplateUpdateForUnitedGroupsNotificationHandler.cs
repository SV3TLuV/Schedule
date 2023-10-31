using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.LessonTemplates.Notifications.LessonTemplateUpdateForUnitedGroups;

public sealed class LessonTemplateUpdateForUnitedGroupsNotificationHandler
    : INotificationHandler<LessonTemplateUpdateForUnitedGroupsNotification>
{
    private readonly IScheduleDbContext _context;

    public LessonTemplateUpdateForUnitedGroupsNotificationHandler(
        IScheduleDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(LessonTemplateUpdateForUnitedGroupsNotification notification,
        CancellationToken cancellationToken)
    {
        var lessonTemplate = await _context.Set<LessonTemplate>()
            .Include(e => e.Template)
            .ThenInclude(e => e.Group)
            .ThenInclude(e => e.GroupGroups)
            .Include(e => e.LessonTemplateTeacherClassrooms)
            .AsNoTrackingWithIdentityResolution()
            .AsSplitQuery()
            .FirstOrDefaultAsync(e => e.LessonTemplateId == notification.LessonTemplateId, cancellationToken);

        if (lessonTemplate is null)
            throw new NotFoundException(nameof(LessonTemplate), notification.LessonTemplateId);

        var unitedGroupIds = lessonTemplate.Template.Group.GroupGroups.Select(e => e.GroupId2);

        var lessonTemplates = await _context.Set<LessonTemplate>()
            .Include(e => e.Template)
            .AsNoTrackingWithIdentityResolution()
            .Where(e =>
                e.Number == lessonTemplate.Number &&
                unitedGroupIds.Contains(e.Template.GroupId) &&
                e.Template.TermId == lessonTemplate.Template.TermId)
            .ToListAsync(cancellationToken);

        await _context.Set<LessonTemplateTeacherClassroom>()
            .Where(e => lessonTemplates
                .Select(l => l.LessonTemplateId)
                .Contains(e.LessonTemplateId))
            .ExecuteDeleteAsync(cancellationToken);
        
        foreach (var lessonTemplateToUpdate in lessonTemplates)
        {
            lessonTemplateToUpdate.TimeId = lessonTemplate.TimeId;
            lessonTemplateToUpdate.Subgroup = lessonTemplate.Subgroup;
            lessonTemplateToUpdate.DisciplineId = lessonTemplate.DisciplineId;
            _context.Set<LessonTemplate>().Update(lessonTemplateToUpdate);
            
            var newTeacherClassrooms = lessonTemplate.LessonTemplateTeacherClassrooms
                .Select(e => new LessonTemplateTeacherClassroom
                {
                    LessonTemplateId = lessonTemplateToUpdate.LessonTemplateId,
                    TeacherId = e.TeacherId,
                    ClassroomId = e.ClassroomId
                })
                .ToArray();
            await _context.Set<LessonTemplateTeacherClassroom>().AddRangeAsync(newTeacherClassrooms, cancellationToken);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}