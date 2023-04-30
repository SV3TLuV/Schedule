using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Lessons.Notifications.CreatedOrUpdated;

public sealed class CreatedOrUpdatedLessonNotificationHandler : INotificationHandler<CreatedOrUpdatedLessonNotification>
{
    private readonly IScheduleDbContext _context;

    public CreatedOrUpdatedLessonNotificationHandler(IScheduleDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(CreatedOrUpdatedLessonNotification notification,
        CancellationToken cancellationToken)
    {
        var lesson = await _context.Set<Lesson>()
            .AsNoTrackingWithIdentityResolution()
            .Include(e => e.Timetable)
            .ThenInclude(e => e.Date)
            .Include(e => e.Timetable)
            .ThenInclude(e => e.Group)
            .ThenInclude(e => e.Term)
            .Include(e => e.LessonTeacherClassrooms)
            .FirstAsync(e => e.LessonId == notification.Id, cancellationToken);
        
        var template = await _context.Set<LessonTemplate>()
            .AsNoTrackingWithIdentityResolution()
            .Include(e => e.Template)
            .ThenInclude(e => e.Group)
            .ThenInclude(e => e.Term)
            .Include(e => e.LessonTemplateTeacherClassrooms)
            .FirstOrDefaultAsync(e => 
                e.Template.GroupId == lesson.Timetable.GroupId &&
                e.Template.DayId == lesson.Timetable.Date.DayId &&
                e.Template.WeekTypeId == lesson.Timetable.Date.WeekTypeId &&
                e.Template.TermId == lesson.Timetable.Group.TermId, cancellationToken);

        lesson.IsChanged = !lesson.Equals(template);
        _context.Set<Lesson>().Update(lesson);
        await _context.SaveChangesAsync(cancellationToken);
    }
}