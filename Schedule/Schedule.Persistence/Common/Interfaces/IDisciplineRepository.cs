using Schedule.Core.Models;

namespace Schedule.Persistence.Common.Interfaces;

public interface IDisciplineRepository
{
    public Task<int> CreateDiscipline(Discipline discipline, CancellationToken cancellationToken = default);
    
    public Task UpdateDiscipline(Discipline discipline, CancellationToken cancellationToken = default);
    
    public Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    
    public Task RestoreAsync(int id, CancellationToken cancellationToken = default);
}