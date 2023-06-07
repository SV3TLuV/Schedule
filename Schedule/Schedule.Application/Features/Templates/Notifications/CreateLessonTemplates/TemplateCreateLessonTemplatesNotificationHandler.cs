using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Features.LessonTemplates.Commands.Create;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Templates.Notifications.CreateLessonTemplates;

public sealed class
    TemplateCreateLessonTemplatesNotificationHandler : INotificationHandler<TemplateCreateLessonTemplatesNotification>
{
    private readonly IScheduleDbContext _context;
    private readonly IMediator _mediator;

    public TemplateCreateLessonTemplatesNotificationHandler(
        IScheduleDbContext context,
        IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task Handle(TemplateCreateLessonTemplatesNotification notification,
        CancellationToken cancellationToken)
    {
        for (var i = 1; i <= 4; i++)
        {
            var command = new CreateLessonTemplateCommand
            {
                Number = i,
                TemplateId = notification.TemplateId
            };
            await _mediator.Send(command, cancellationToken);
        }
    }
}