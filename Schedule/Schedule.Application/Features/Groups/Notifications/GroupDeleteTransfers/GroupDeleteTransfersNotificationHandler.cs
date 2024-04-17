using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Notifications.GroupDeleteTransfers;

public sealed class GroupDeleteTransfersNotificationHandler(IScheduleDbContext context)
    : INotificationHandler<GroupDeleteTransfersNotification>
{
    public async Task Handle(GroupDeleteTransfersNotification notification,
        CancellationToken cancellationToken)
    {
        await context.GroupTransfers
            .AsNoTracking()
            .Where(e => e.GroupId == notification.Id)
            .ExecuteDeleteAsync(cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}