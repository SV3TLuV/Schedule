using Schedule.Core.Models;

namespace Schedule.Persistence.Common.Interfaces;

public interface IEmployeeRepository : IRepository
{
    public Task<int> CreateAsync(Employee employee, CancellationToken cancellationToken = default);
    public Task UpdateAsync(Employee employee, CancellationToken cancellationToken = default);
    public Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    public Task RestoreAsync(int id, CancellationToken cancellationToken = default);
}