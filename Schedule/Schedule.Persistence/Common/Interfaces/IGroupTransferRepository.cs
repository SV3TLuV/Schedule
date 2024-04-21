using Schedule.Core.Models;

namespace Schedule.Persistence.Common.Interfaces;

public interface IGroupTransferRepository : IRepository
{
    public Task MarkAsTransferedAsync(GroupTransfer groupTransfer, CancellationToken cancellationToken = default);
    public Task CreateForGroup(int groupId, CancellationToken cancellationToken = default);
    public Task DeleteByGroupId(int groupId, CancellationToken cancellationToken = default);
}