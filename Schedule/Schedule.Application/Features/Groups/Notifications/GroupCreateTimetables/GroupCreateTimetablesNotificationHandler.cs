﻿using MediatR;
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
        var group = await _context.Set<Group>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.GroupId == notification.Id, cancellationToken);

        if (group is null)
            throw new NotFoundException(nameof(Group), notification.Id);

        var dateIds = await _context.Set<Date>()
            .AsNoTrackingWithIdentityResolution()
            .Where(e => e.Value.Date >= _dateInfoService.CurrentDateTime.Date)
            .Select(e => e.DateId)
            .ToListAsync(cancellationToken);

        foreach (var dateId in dateIds)
        {
            var command = new CreateTimetableCommand
            {
                GroupId = group.GroupId,
                DateId = dateId
            };
            await _mediator.Send(command, cancellationToken);
        }
    }
}