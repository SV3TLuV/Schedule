using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Features.Sessions.Commands.Delete;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Users.Notifications.UserSessionRevocation;

public sealed class UserSessionRevocationNotificationHandler
    : INotificationHandler<UserSessionRevocationNotification>
{
    private readonly IScheduleDbContext _context;
    private readonly IMediator _mediator;

    public UserSessionRevocationNotificationHandler(
        IScheduleDbContext context,
        IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }
    
    public async Task Handle(UserSessionRevocationNotification notification,
        CancellationToken cancellationToken)
    {
        var sessionIds = await _context.Set<Session>()
            .AsNoTrackingWithIdentityResolution()
            .Where(e => e.UserId == notification.UserId)
            .Select(e => e.SessionId)
            .ToListAsync(cancellationToken);

        foreach (var sessionId in sessionIds)
        {
            var command = new DeleteSessionCommand(sessionId);
            await _mediator.Send(command, cancellationToken);
        }
    }
}