using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Features.Timetables.Commands.Create;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Dates.Notifications.CreateTimetables;

public sealed class DateCreateTimetablesNotificationHandler 
    : INotificationHandler<DateCreateTimetablesNotification>
{
    private readonly IScheduleDbContext _context;
    private readonly IMediator _mediator;

    public DateCreateTimetablesNotificationHandler(
        IScheduleDbContext context,
        IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }
    
    public async Task Handle(DateCreateTimetablesNotification notification, CancellationToken cancellationToken)
    {
        var groupIds = await _context.Set<Group>()
            .AsNoTrackingWithIdentityResolution()
            .Select(e => e.GroupId)
            .ToListAsync(cancellationToken);
        
        var commands = groupIds
            .Select(groupId => new CreateTimetableCommand
            {
                GroupId = groupId,
                DateId = notification.DateId
            });

        foreach (var command in commands)
            await _mediator.Send(command, cancellationToken);
    }
}