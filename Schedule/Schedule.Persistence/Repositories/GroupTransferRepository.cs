using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class GroupTransferRepository(IScheduleDbContext context) : Repository(context), IGroupTransferRepository
{
    public async Task MarkAsTransferedAsync(GroupTransfer groupTransfer, CancellationToken cancellationToken = default)
    {
        groupTransfer.IsTransferred = true;

        Context.GroupTransfers.Update(groupTransfer);
        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task CreateForGroup(int groupId, CancellationToken cancellationToken = default)
    {
        var group = await Context.Groups
            .Include(e => e.Speciality)
            .FirstOrDefaultAsync(e => e.GroupId == groupId, cancellationToken);

        if (group is null)
        {
            throw new NotFoundException(nameof(group), groupId);
        }

        for (var i = group.TermId; i < group.Speciality.MaxTermId; i++)
        {
            var nextTermId = i + 1;

            await Context.GroupTransfers.AddAsync(new GroupTransfer
            {
                GroupId = group.GroupId,
                NextTermId = nextTermId,
                IsTransferred = false,
                TransferDate = GetTransferDate(group.EnrollmentYear, group.TermId, nextTermId, group.IsAfterEleven)
            }, cancellationToken);
        }

        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteByGroupId(int groupId, CancellationToken cancellationToken = default)
    {
        await Context.GroupTransfers
            .Where(e => e.GroupId == groupId)
            .ExecuteDeleteAsync(cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
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