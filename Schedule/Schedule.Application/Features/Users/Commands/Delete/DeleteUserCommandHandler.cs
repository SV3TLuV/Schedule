using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Features.Users.Notifications.UserSessionRevocation;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Users.Commands.Delete;

public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
{
    private readonly IScheduleDbContext _context;
    private readonly IMediator _mediator;

    public DeleteUserCommandHandler(
        IScheduleDbContext context,
        IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Set<User>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.UserId == request.Id, cancellationToken);

        if (user is null)
            throw new NotFoundException(nameof(User), request.Id);

        _context.Set<User>().Remove(user);
        await _context.SaveChangesAsync(cancellationToken);
        await _mediator.Publish(new UserSessionRevocationNotification(request.Id), cancellationToken);
        return Unit.Value;
    }
}