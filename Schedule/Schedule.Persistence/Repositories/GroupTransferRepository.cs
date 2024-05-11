using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class GroupTransferRepository(IScheduleDbContext context) : IGroupTransferRepository
{
    public async Task MarkAsTransferedAsync(GroupTransfer groupTransfer, CancellationToken cancellationToken = default)
    {
        groupTransfer.IsTransferred = true;

        context.GroupTransfers.Update(groupTransfer);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task CreateForGroup(int groupId, CancellationToken cancellationToken = default)
    {
        var group = await context.Groups
            .Include(e => e.Speciality)
            .FirstOrDefaultAsync(e => e.GroupId == groupId, cancellationToken);

        if (group is null)
        {
            throw new NotFoundException(nameof(group), groupId);
        }

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

    public async Task DeleteByGroupId(int groupId, CancellationToken cancellationToken = default)
    {
        await context.GroupTransfers
            .Where(e => e.GroupId == groupId)
            .ExecuteDeleteAsync(cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    private DateOnly GetTransferDate(int enrollmentYear, int groupTermId, int nextTermId, bool isAfterEleven)
    {
        var startTermId = isAfterEleven ? 3 : 1;
        var termOffset = nextTermId - groupTermId;
        var transferYear = enrollmentYear + Convert.ToInt32(Math.Ceiling((nextTermId - startTermId) / 2.0));
        var transferMonth = termOffset % 2 == 0 ? 8 : 1;
        return DateOnly.FromDateTime(new DateTime(transferYear, transferMonth, 1));
    }
}