using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Notifications.GroupCreateTransfers;

public sealed class GroupCreateTransfersNotificationHandler 
    : INotificationHandler<GroupCreateTransfersNotification>
{
    private readonly IScheduleDbContext _context;
    private readonly IDateInfoService _dateInfoService;

    public GroupCreateTransfersNotificationHandler(
        IScheduleDbContext context,
        IDateInfoService dateInfoService)
    {
        _context = context;
        _dateInfoService = dateInfoService;
    }
    
    public async Task Handle(GroupCreateTransfersNotification notification,
        CancellationToken cancellationToken)
    {
        var group = await _context.Set<Group>()
            .AsNoTrackingWithIdentityResolution()
            .Include(e => e.Speciality)
            .FirstOrDefaultAsync(e => e.GroupId == notification.Id, cancellationToken);

        if (group is null)
            throw new NotFoundException(nameof(Group), notification.Id);

        for (var i = group.TermId; i < group.Speciality.MaxTermId; i++)
        {
            var year = _dateInfoService.CurrentDateTime.Year;
            var nextTermId = i + 1;
            
            await _context.Set<GroupTransfer>().AddAsync(new GroupTransfer
            {
                GroupId = group.GroupId,
                NextTermId = nextTermId,
                IsTransferred = false,
                TransferDate = (nextTermId & 1) == 0 
                    ? new DateTime(year, 8, 1)
                    : new DateTime(year, 1, 1),
            }, cancellationToken);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}