using Microsoft.EntityFrameworkCore;
using Quartz;
using Schedule.Core.Common.Interfaces;

namespace Schedule.Application.Jobs;

public sealed class TransferGroupsJob(
    IScheduleDbContext context,
    IDateInfoService dateInfoService) : IJob
{
    public async Task Execute(IJobExecutionContext jobContext)
    {
        await using var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            var groups = await context.Groups
                .AsNoTracking()
                .Include(e => e.Speciality)
                .Where(e => !e.IsDeleted && e.TermId < e.Speciality.MaxTermId)
                .ToListAsync();

            foreach (var group in groups)
            {
                var transfer = await context.GroupTransfers
                    .AsNoTrackingWithIdentityResolution()
                    .OrderBy(e => e.NextTermId)
                    .FirstOrDefaultAsync(e => e.GroupId == group.GroupId && !e.IsTransferred);

                if (transfer is null)
                    continue;

                if (dateInfoService.CurrentDate < transfer.TransferDate)
                    continue;

                group.TermId = transfer.NextTermId;
                context.Groups.Update(group);

                transfer.IsTransferred = true;
                context.GroupTransfers.Update(transfer);

                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
        }
        catch
        {
            await transaction.RollbackAsync();
        }
    }
}