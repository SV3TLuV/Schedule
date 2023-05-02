using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Features.Timetables.Commands.Create;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Templates.Notifications.CreateTimetables;

public sealed class CreateTimetablesNotificationHandler
    : INotificationHandler<CreateTimetablesNotification>
{
    private readonly IScheduleDbContext _context;
    private readonly IDateInfoService _dateInfoService;
    private readonly IMediator _mediator;

    public CreateTimetablesNotificationHandler(
        IScheduleDbContext context,
        IDateInfoService dateInfoService,
        IMediator mediator)
    {
        _context = context;
        _dateInfoService = dateInfoService;
        _mediator = mediator;
    }
    
    public async Task Handle(CreateTimetablesNotification notification,
        CancellationToken cancellationToken)
    {
        var template = await _context.Set<Template>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => 
                e.TemplateId == notification.TemplateId, cancellationToken);
        
        if (template is null)
            throw new NotFoundException(nameof(Template), notification.TemplateId);

        var dateIds = await _context.Set<Date>()
            .AsNoTrackingWithIdentityResolution()
            .Where(e => e.Value >= _dateInfoService.CurrentDateTime.Date)
            .Select(e => e.DateId)
            .ToListAsync(cancellationToken);
        
        foreach (var dateId in dateIds)
        {
            var command = new CreateTimetableCommand
            {
                GroupId = template.GroupId,
                DateId = dateId
            };
            await _mediator.Send(command, cancellationToken);
        }
    }
}