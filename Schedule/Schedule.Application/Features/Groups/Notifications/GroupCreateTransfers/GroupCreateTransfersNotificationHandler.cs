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

    public GroupCreateTransfersNotificationHandler(
        IScheduleDbContext context)
    {
        _context = context;
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
            var nextTermId = i + 1;

            await _context.Set<GroupTransfer>().AddAsync(new GroupTransfer
            {
                GroupId = group.GroupId,
                NextTermId = nextTermId,
                IsTransferred = false,
                TransferDate = GetTransferDate(group.EnrollmentYear, group.TermId, nextTermId)
            }, cancellationToken);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }

    private static DateTime GetTransferDate(int enrollmentYear, int startTermId, int termId)
    {
        var termOffset = termId - startTermId;
        var transferYear = enrollmentYear + Convert.ToInt32(Math.Floor((double)(termOffset + 1) / 2));
        var transferMonth = termOffset % 2 == 0 ? 8 : 1;
        return new DateTime(transferYear, transferMonth, 1);
    }
}