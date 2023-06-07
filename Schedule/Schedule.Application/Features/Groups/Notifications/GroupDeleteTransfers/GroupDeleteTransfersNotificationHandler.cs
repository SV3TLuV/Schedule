using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Notifications.GroupDeleteTransfers;

public sealed class GroupDeleteTransfersNotificationHandler : INotificationHandler<GroupDeleteTransfersNotification>
{
    private readonly IScheduleDbContext _context;

    public GroupDeleteTransfersNotificationHandler(
        IScheduleDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(GroupDeleteTransfersNotification notification,
        CancellationToken cancellationToken)
    {
        await _context.Set<GroupTransfer>()
            .AsNoTrackingWithIdentityResolution()
            .Where(e => e.GroupId == notification.Id)
            .ExecuteDeleteAsync(cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}