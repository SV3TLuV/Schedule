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
        var lesson = await _context.Set<Lesson>()
            .AsNoTrackingWithIdentityResolution()
            .Include(e => e.Timetable)
            .Include(e => e.LessonTeacherClassrooms)
            .FirstAsync(e => e.LessonId == notification.Id, cancellationToken);
        
        var timetable = lesson.Timetable;
    }
}