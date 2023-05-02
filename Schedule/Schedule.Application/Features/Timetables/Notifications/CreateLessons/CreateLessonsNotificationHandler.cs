using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Features.Lessons.Commands.Create;
using Schedule.Application.Features.Templates.Notifications.CreateLessonTemplates;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Timetables.Notifications.CreateLessons;

public sealed class CreateLessonsNotificationHandler : INotificationHandler<CreateLessonTemplatesNotification>
{
    private readonly IScheduleDbContext _context;
    private readonly IMediator _mediator;

    public CreateLessonsNotificationHandler(
        IScheduleDbContext context,
        IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }
    
    public async Task Handle(CreateLessonTemplatesNotification notification, CancellationToken cancellationToken)
    {
        var timetable = await _context.Set<Timetable>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.TimetableId == notification.TemplateId, cancellationToken);

        if (timetable is null)
        {
            throw new NotFoundException(nameof(Template), notification.TemplateId);
        }

        for (var i = 1; i <= 4; i++)
        {
            var command = new CreateLessonCommand
            {
                Number = i,
                TimetableId = notification.TemplateId,
            };
            await _mediator.Send(command, cancellationToken);
        }
    }
}