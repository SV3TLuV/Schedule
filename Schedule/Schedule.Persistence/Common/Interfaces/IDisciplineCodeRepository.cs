using Schedule.Core.Models;

namespace Schedule.Persistence.Common.Interfaces;

public interface IDisciplineCodeRepository
{
    public Task<int> AddIfNotExistAsync(string code, CancellationToken cancellationToken = default);

    public Task UpdateAsync(DisciplineCode disciplineCode, CancellationToken cancellationToken = default);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default);

    public Task RestoreAsync(int id, CancellationToken cancellationToken = default);
}