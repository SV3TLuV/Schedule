using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Common.Interfaces;
using Schedule.Application.Features.Timetables.Commands.Create;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Notifications.GroupCreateTimetables;

public sealed class GroupCreateTimetablesNotificationHandler
    : INotificationHandler<GroupCreateTimetablesNotification>
{
    private readonly IScheduleDbContext _context;
    private readonly IDateInfoService _dateInfoService;
    private readonly IMediator _mediator;

    public GroupCreateTimetablesNotificationHandler(
        IScheduleDbContext context,
        IDateInfoService dateInfoService,
        IMediator mediator)
    {
        _context = context;
        _dateInfoService = dateInfoService;
        _mediator = mediator;
    }

    public async Task Handle(GroupCreateTimetablesNotification notification,
        CancellationToken cancellationToken)
    {
        var dateIds = await _context.Set<Date>()
            .AsNoTrackingWithIdentityResolution()
            .Where(e => e.Value.Date >= _dateInfoService.CurrentDateTime.Date)
            .Select(e => e.DateId)
            .ToListAsync(cancellationToken);

        var commands = dateIds.Select(id =>
            new CreateTimetableCommand
            {
                GroupId = notification.Id,
                DateId = id
            });
        
        foreach (var command in commands)
        {
            await _mediator.Send(command, cancellationToken);
        }
    }
}