using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Features.Groups.Notifications.GroupDeleteTransfers;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Commands.Delete;

public sealed class DeleteGroupCommandHandler(
    IScheduleDbContext context,
    IMediator mediator) : IRequestHandler<DeleteGroupCommand, Unit>
{
    public async Task<Unit> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await context.Groups
            .FirstOrDefaultAsync(e => e.GroupId == request.Id, cancellationToken);

        if (group is null)
            throw new NotFoundException(nameof(Group), request.Id);

        group.IsDeleted = true;
        
        context.Groups.Update(group);
        await context.SaveChangesAsync(cancellationToken);
        await mediator.Publish(new GroupDeleteTransfersNotification(group.GroupId), cancellationToken);
        return Unit.Value;
    }
}