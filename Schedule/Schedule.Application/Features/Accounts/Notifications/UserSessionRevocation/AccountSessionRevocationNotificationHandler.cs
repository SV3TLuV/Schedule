using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Features.Sessions.Commands.Delete;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Accounts.Notifications.UserSessionRevocation;

public sealed class AccountSessionRevocationNotificationHandler(
    IScheduleDbContext context,
    IMediator mediator) : INotificationHandler<UserSessionRevocationNotification>
{
    public async Task Handle(UserSessionRevocationNotification notification,
        CancellationToken cancellationToken)
    {
        var sessionIds = await context.Sessions
            .AsNoTracking()
            .Where(e => e.AccountId == notification.AccountId)
            .Select(e => e.SessionId)
            .ToListAsync(cancellationToken);

        foreach (var sessionId in sessionIds)
        {
            var command = new DeleteSessionCommand(sessionId);
            await mediator.Send(command, cancellationToken);
        }
    }
}