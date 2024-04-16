using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Features.Groups.Notifications.GroupCreateTransfers;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Commands.Restore;

public sealed class RestoreGroupCommandHandler(
    IScheduleDbContext context,
    IMediator mediator) : IRequestHandler<RestoreGroupCommand, Unit>
{
    public async Task<Unit> Handle(RestoreGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await context.Groups
            .FirstOrDefaultAsync(e => e.GroupId == request.Id, cancellationToken);

        if (group is null)
            throw new NotFoundException(nameof(Group), request.Id);

        group.IsDeleted = false;

        context.Groups.Update(group);
        await context.SaveChangesAsync(cancellationToken);
        await mediator.Publish(new GroupCreateTransfersNotification(group.GroupId), cancellationToken);
        return Unit.Value;
    }
}