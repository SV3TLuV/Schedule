using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Features.Groups.Notifications.GroupCreateTransfers;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Commands.Restore;

public sealed class RestoreGroupCommandHandler : IRequestHandler<RestoreGroupCommand, Unit>
{
    private readonly IScheduleDbContext _context;
    private readonly IMediator _mediator;

    public RestoreGroupCommandHandler(
        IScheduleDbContext context,
        IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(RestoreGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _context.Set<Group>()
            .FirstOrDefaultAsync(e => e.GroupId == request.Id, cancellationToken);

        if (group is null)
            throw new NotFoundException(nameof(Group), request.Id);

        group.IsDeleted = false;
        _context.Set<Group>().Update(group);
        await _context.SaveChangesAsync(cancellationToken);
        await _mediator.Publish(new GroupCreateTransfersNotification(group.GroupId), cancellationToken);
        return Unit.Value;
    }
}