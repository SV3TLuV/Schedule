using Microsoft.EntityFrameworkCore;
using Quartz;
using Schedule.Core.Common.Interfaces;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Jobs;

public sealed class TransferGroupsJob(
    IScheduleDbContext context,
    IGroupRepository groupRepository,
    IGroupTransferRepository groupTransferRepository,
    IDateInfoService dateInfoService) : IJob
{
    public async Task Execute(IJobExecutionContext jobContext)
    {
        await context.WithTransactionAsync(async () =>
        {
            var groups = await context.Groups
                .AsNoTracking()
                .Include(e => e.Speciality)
                .Where(e => !e.IsDeleted && e.TermId < e.Speciality.MaxTermId)
                .ToListAsync();

            foreach (var group in groups)
            {
                var transfer = await context.GroupTransfers
                    .OrderBy(e => e.NextTermId)
                    .FirstOrDefaultAsync(e => e.GroupId == group.GroupId && !e.IsTransferred);

                if (transfer is null)
                    continue;

                if (dateInfoService.CurrentDate < transfer.TransferDate)
                    continue;

                group.TermId = transfer.NextTermId;
                await groupRepository.UpdateAsync(group);

                await groupTransferRepository.MarkAsTransferedAsync(transfer);

                await context.SaveChangesAsync();
            }
        });
    }
}