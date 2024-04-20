using Schedule.Core.Models;

namespace Schedule.Persistence.Common.Interfaces;

public interface IDisciplineRepository : IRepository
{
    public Task<int> CreateAsync(Discipline discipline, CancellationToken cancellationToken = default);
    
    public Task UpdateAsync(Discipline discipline, CancellationToken cancellationToken = default);
    
    public Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    
    public Task RestoreAsync(int id, CancellationToken cancellationToken = default);
}