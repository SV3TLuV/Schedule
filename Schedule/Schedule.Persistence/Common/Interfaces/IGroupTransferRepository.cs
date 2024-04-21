using Schedule.Core.Models;

namespace Schedule.Persistence.Common.Interfaces;

public interface IGroupTransferRepository : IRepository
{
    //public Task CreateAsync(GroupTransfer groupTransfer, CancellationToken cancellationToken = default);
    //public Task MarkAsTransferedAsync(GroupTransfer groupTransfer, CancellationToken cancellationToken = default);
    //public Task DeleteAsync(GroupTransfer groupTransfer, CancellationToken cancellationToken = default);

    public Task CreateForGroup(int groupId, CancellationToken cancellationToken = default);
    public Task DeleteByGroupId(int groupId, CancellationToken cancellationToken = default);
}