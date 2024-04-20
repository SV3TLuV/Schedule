using Schedule.Core.Models;

namespace Schedule.Persistence.Common.Interfaces;

public interface IDisciplineNameRepository : IRepository
{
    public Task AddIfNotExistAsync(string name, CancellationToken cancellationToken = default);

    public Task UpdateAsync(DisciplineName disciplineName, CancellationToken cancellationToken = default);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    
    public Task RestoreAsync(int id, CancellationToken cancellationToken = default);
}