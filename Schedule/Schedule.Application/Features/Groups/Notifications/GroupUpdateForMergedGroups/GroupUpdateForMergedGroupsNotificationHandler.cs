using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Notifications.GroupUpdateForMergedGroups;

public sealed class GroupUpdateForMergedGroupsNotificationHandler
    : INotificationHandler<GroupUpdateForMergedGroupsNotification>
{
    private readonly IScheduleDbContext _context;

    public GroupUpdateForMergedGroupsNotificationHandler(
        IScheduleDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(GroupUpdateForMergedGroupsNotification notification,
        CancellationToken cancellationToken)
    {
        var group = await _context.Set<Group>()
            .AsNoTracking()
            .Include(e => e.GroupGroups)
            .FirstOrDefaultAsync(e => e.GroupId == notification.Id, cancellationToken);

        if (group is null)
            throw new NotFoundException(nameof(Group), notification.Id);

        var groupIds = group.GroupGroups
            .Select(e => e.GroupId2)
            .Concat(new [] { group.GroupId })
            .Distinct()
            .ToArray();

        foreach (var groupGroup in group.GroupGroups)
        {
            var groupId = groupGroup.GroupId2;
            
            await _context.Set<GroupGroup>()
                .Where(e => e.GroupId == groupId)
                .AsNoTrackingWithIdentityResolution()
                .ExecuteDeleteAsync(cancellationToken);
            
            var groupGroups = groupIds
                .Where(id => id != groupId)
                .Select(id => new GroupGroup
                {
                    GroupId = groupId,
                    GroupId2 = id,
                });

            await _context.Set<GroupGroup>().AddRangeAsync(groupGroups, cancellationToken);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}