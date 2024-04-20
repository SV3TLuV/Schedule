using Schedule.Core.Models;

namespace Schedule.Persistence.Common.Interfaces;

public interface IDisciplineTypeRepository : IRepository
{
    public Task AddIfNotExistAsync(string name, CancellationToken cancellationToken = default);

    public Task UpdateAsync(DisciplineType disciplineType, CancellationToken cancellationToken = default);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}