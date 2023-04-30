using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.LessonTemplates.Notifications.Deleted;

public sealed class DeletedLessonTemplateNotificationHandler 
    : INotificationHandler<DeletedLessonTemplateNotification>
{
    private readonly IScheduleDbContext _context;

    public DeletedLessonTemplateNotificationHandler(
        IScheduleDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(DeletedLessonTemplateNotification notification,
        CancellationToken cancellationToken)
    {
        var lessonTemplate = notification.LessonTemplate;
        var template = await _context.Set<Template>()
            .Include(e => e.Group)
            .Include(e => e.Term)
            .AsNoTrackingWithIdentityResolution()
            .FirstAsync(e => e.TemplateId == lessonTemplate.TemplateId,
                cancellationToken);

        var timetables = await _context.Set<Timetable>()
            .AsNoTrackingWithIdentityResolution()
            .Include(e => e.Date)
            .Where(e => 
                e.Date.DayId == template.DayId &&
                e.Date.WeekTypeId == template.WeekTypeId &&
                e.Date.Term == template.TermId &&
                e.Date.Value >= DateTime.Now &&
                e.GroupId == template.GroupId)
            .ToListAsync(cancellationToken);

        foreach (var timetable in timetables)
        {
            var lesson = timetable.Lessons
                .FirstOrDefault(e => e.Number == lessonTemplate.Number);

            if (lesson is null)
                continue;
            
            lesson.IsChanged = true;
            _context.Set<Lesson>().Update(lesson);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}