using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Notifications.GroupCreateTransfers;

public sealed class GroupCreateTransfersNotificationHandler(IScheduleDbContext context)
    : INotificationHandler<GroupCreateTransfersNotification>
{
    public async Task Handle(GroupCreateTransfersNotification notification,
        CancellationToken cancellationToken)
    {
        var group = await context.Groups
            .AsNoTrackingWithIdentityResolution()
            .Include(e => e.Speciality)
            .FirstOrDefaultAsync(e => e.GroupId == notification.Id, cancellationToken);

        if (group is null)
            throw new NotFoundException(nameof(Group), notification.Id);

        for (var i = group.TermId; i < group.Speciality.MaxTermId; i++)
        {
            var nextTermId = i + 1;

            await context.GroupTransfers.AddAsync(new GroupTransfer
            {
                GroupId = group.GroupId,
                NextTermId = nextTermId,
                IsTransferred = false,
                TransferDate = GetTransferDate(group.EnrollmentYear, group.TermId, nextTermId, group.IsAfterEleven)
            }, cancellationToken);
        }

        await context.SaveChangesAsync(cancellationToken);
    }

    private static DateOnly GetTransferDate(int enrollmentYear, int groupTermId, int nextTermId, bool isAfterEleven)
    {
        var startTermId = isAfterEleven ? 3 : 1;
        var termOffset = nextTermId - groupTermId;
        var transferYear = enrollmentYear + Convert.ToInt32(Math.Ceiling((nextTermId - startTermId) / 2.0));
        var transferMonth = termOffset % 2 == 0 ? 8 : 1;
        return DateOnly.FromDateTime(new DateTime(transferYear, transferMonth, 1));
    }
}