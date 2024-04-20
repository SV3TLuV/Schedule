using Schedule.Core.Models;

namespace Schedule.Persistence.Common.Interfaces;

public interface IGroupRepository : IRepository
{
    public Task<int> CreateAsync(Group group, CancellationToken cancellationToken = default);
    public Task UpdateAsync(Group group, CancellationToken cancellationToken = default);
    public Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    public Task RestoreAsync(int id, CancellationToken cancellationToken = default);
}