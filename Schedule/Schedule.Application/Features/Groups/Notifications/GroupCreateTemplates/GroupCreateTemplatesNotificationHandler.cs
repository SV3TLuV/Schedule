using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Features.Templates.Commands.Create;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Notifications.GroupCreateTemplates;

public sealed class GroupCreateTemplatesNotificationHandler
    : INotificationHandler<GroupCreateTemplatesNotification>
{
    private readonly IScheduleDbContext _context;
    private readonly IMediator _mediator;

    public GroupCreateTemplatesNotificationHandler(
        IScheduleDbContext context,
        IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task Handle(GroupCreateTemplatesNotification notification, CancellationToken cancellationToken)
    {
        var group = await _context.Set<Group>()
            .Include(e => e.Speciality)
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.GroupId == notification.Id, cancellationToken);

        if (group is null)
            throw new NotFoundException(nameof(Group), notification.Id);

        var ids = await _context.Set<Day>()
            .Join(_context.Set<Term>(),
                d => true,
                t => true,
                (d, t) => new { d.DayId, t.TermId })
            .Join(_context.Set<WeekType>(),
                d => true,
                w => true,
                (d, w) => new { d.DayId, d.TermId, w.WeekTypeId })
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync(cancellationToken);

        var commands = ids.Select(item =>
            new CreateTemplateCommand
            {
                GroupId = notification.Id,
                TermId = item.TermId,
                DayId = item.DayId,
                WeekTypeId = item.WeekTypeId
            });

        foreach (var command in commands)
        {
            await _mediator.Send(command, cancellationToken);
        }
    }
}