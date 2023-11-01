using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Features.Groups.Notifications.GroupDeleteTransfers;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Commands.Delete;

public sealed class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand, Unit>
{
    private readonly IScheduleDbContext _context;
    private readonly IMediator _mediator;

    public DeleteGroupCommandHandler(
        IScheduleDbContext context,
        IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _context.Set<Group>()
            .FirstOrDefaultAsync(e => e.GroupId == request.Id, cancellationToken);

        if (group is null)
            throw new NotFoundException(nameof(Group), request.Id);

        await _context.Set<GroupGroup>()
            .Where(entity =>
                entity.GroupId == request.Id ||
                entity.GroupId2 == request.Id)
            .AsNoTrackingWithIdentityResolution()
            .ExecuteDeleteAsync(cancellationToken);

        group.IsDeleted = true;
        
        _context.Set<Group>().Update(group);
        await _context.SaveChangesAsync(cancellationToken);
        await _mediator.Publish(new GroupDeleteTransfersNotification(group.GroupId), cancellationToken);
        return Unit.Value;
    }
}