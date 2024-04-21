using Schedule.Core.Models;

namespace Schedule.Persistence.Common.Interfaces;

public interface IGroupTransferRepository
{
    public Task CreateAsync(GroupTransfer groupTransfer, CancellationToken cancellationToken = default);
    public Task MarkAsTransferedAsync(GroupTransfer groupTransfer, CancellationToken cancellationToken = default);
    public Task DeleteAsync(GroupTransfer groupTransfer, CancellationToken cancellationToken = default);
}