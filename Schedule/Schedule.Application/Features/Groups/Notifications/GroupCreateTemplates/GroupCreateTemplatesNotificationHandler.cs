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

        var dayIds = await _context.Set<Day>()
            .AsNoTrackingWithIdentityResolution()
            .Select(e => e.DayId)
            .ToListAsync(cancellationToken);
        
        var weekTypeIds = await _context.Set<WeekType>()
            .AsNoTrackingWithIdentityResolution()
            .Select(e => e.WeekTypeId)
            .ToListAsync(cancellationToken);

        var termIds = await _context.Set<Term>()
            .AsNoTrackingWithIdentityResolution()
            .Where(e => e.TermId <= group.TermId)
            .Select(e => e.TermId)
            .ToListAsync(cancellationToken);

        var commands = new List<CreateTemplateCommand>();
        
        foreach (var weekTypeId in weekTypeIds)
            foreach (var dayId in dayIds)
                foreach (var termId in termIds)
                    commands.Add(new CreateTemplateCommand
                    {
                        GroupId = notification.Id,
                        TermId = termId,
                        DayId = dayId,
                        WeekTypeId = weekTypeId
                    });

        foreach (var command in commands)
            await _mediator.Send(command, cancellationToken);
    }
}