using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Lessons.Notifications.Created;

public sealed record CreatedLessonNotification(int Id) : INotification;

public sealed class CreatedLessonNotificationHandler : INotificationHandler<CreatedLessonNotification>
{
    private readonly IScheduleDbContext _context;
    private readonly IMediator _mediator;

    public CreatedLessonNotificationHandler(IScheduleDbContext context,
        IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }
    
    public async Task Handle(CreatedLessonNotification notification,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        /*var lesson = await _context.Set<Lesson>()
            .AsNoTrackingWithIdentityResolution()
            .Include(e => e.Timetable)
            .ThenInclude(e => e.Date)
            .Include(e => e.Timetable)
            .ThenInclude(e => e.Group)
            .Include(e => e.LessonTeacherClassrooms)
            .FirstAsync(e => e.LessonId == notification.Id, cancellationToken);
        
        var templateLesson = await _context.Set<LessonTemplate>()
            .AsNoTrackingWithIdentityResolution()
            .Include(e => e.Template)
            .ThenInclude(e => e.Group)
            .ThenInclude(e => e.Course)
            .Include(e => e.LessonTemplateTeacherClassrooms)
            .FirstAsync(e => 
                e.Template.GroupId == lesson.Timetable.GroupId &&
                e.Template.DayId == lesson.Timetable.Date.DayId &&
                e.Template.WeekTypeId == lesson.Timetable.Date.WeekTypeId &&
                e.Template.Group.Course);*/
    }
}