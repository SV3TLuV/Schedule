using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.LessonTemplates.Notifications.CreatedOrUpdated;

public sealed class CreatedOrUpdatedLessonTemplateNotificationHandler 
    : INotificationHandler<CreatedOrUpdatedLessonTemplateNotification>
{
    private readonly IScheduleDbContext _context;
    private readonly IDateInfoService _dateInfoService;

    public CreatedOrUpdatedLessonTemplateNotificationHandler(
        IScheduleDbContext context,
        IDateInfoService dateInfoService)
    {
        _context = context;
        _dateInfoService = dateInfoService;
    }
    
    public async Task Handle(CreatedOrUpdatedLessonTemplateNotification notification, 
        CancellationToken cancellationToken)
    {
        var template = await _context.Set<LessonTemplate>()
            .Include(e => e.Template)
            .ThenInclude(e => e.Group)
            .ThenInclude(e => e.Term)
            .Include(e => e.LessonTemplateTeacherClassrooms)
            .AsNoTrackingWithIdentityResolution()
            .FirstAsync(e => e.LessonTemplateId == notification.Id, cancellationToken);

        var lessons = await _context.Set<Lesson>()
            .Include(e => e.Timetable)
            .ThenInclude(e => e.Date)
            .Include(e => e.Timetable)
            .ThenInclude(e => e.Group)
            .ThenInclude(e => e.Term)
            .Include(e => e.LessonTeacherClassrooms)
            .AsNoTrackingWithIdentityResolution()
            .Where(e => 
                e.Timetable.Date.DayId == template.Template.DayId &&
                e.Timetable.Date.WeekTypeId == template.Template.WeekTypeId &&
                e.Timetable.Date.Term == template.Template.TermId &&
                e.Timetable.Date.Value >= _dateInfoService.CurrentDateTime.Date &&
                e.Timetable.GroupId == template.Template.GroupId)
            .ToListAsync(cancellationToken);

        foreach (var lesson in lessons)
        {
            lesson.IsChanged = !lesson.Equals(template);
            _context.Set<Lesson>().Update(lesson);
        }
        
        await _context.SaveChangesAsync(cancellationToken);
    }
}