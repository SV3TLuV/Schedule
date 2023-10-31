using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.LessonTemplates.Notifications.LessonTemplateCreateForUnitedGroups;

public sealed class LessonTemplateCreateForUnitedGroupsNotificationHandler
    : INotificationHandler<LessonTemplateCreateForUnitedGroupsNotification>
{
    private readonly IScheduleDbContext _context;

    public LessonTemplateCreateForUnitedGroupsNotificationHandler(
        IScheduleDbContext context)
    {
        _context = context;
    }

    public async Task Handle(LessonTemplateCreateForUnitedGroupsNotification notification,
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

        var templateIds = await _context.Set<Template>()
            .AsNoTrackingWithIdentityResolution()
            .Where(e =>
                unitedGroupIds.Contains(e.GroupId) &&
                e.TermId == lessonTemplate.Template.TermId)
            .Select(e => e.TemplateId)
            .ToListAsync(cancellationToken);

        foreach (var templateId in templateIds)
        {
            var newLessonTemplate = new LessonTemplate
            {
                Number = lessonTemplate.Number,
                Subgroup = lessonTemplate.Subgroup,
                TimeId = lessonTemplate.TimeId,
                TemplateId = templateId,
                DisciplineId = lessonTemplate.DisciplineId
            };
            await _context.Set<LessonTemplate>().AddAsync(newLessonTemplate, cancellationToken);
            
            var newTeacherClassrooms = lessonTemplate.LessonTemplateTeacherClassrooms
                .Select(e => new LessonTemplateTeacherClassroom
                {
                    LessonTemplateId = newLessonTemplate.LessonTemplateId,
                    TeacherId = e.TeacherId,
                    ClassroomId = e.ClassroomId
                })
                .ToArray();
            await _context.Set<LessonTemplateTeacherClassroom>().AddRangeAsync(newTeacherClassrooms, cancellationToken);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}