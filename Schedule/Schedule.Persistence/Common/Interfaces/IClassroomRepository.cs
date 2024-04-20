using Schedule.Core.Models;

namespace Schedule.Persistence.Common.Interfaces;

public interface IClassroomRepository : IRepository
{
    public Task AddIfNotExists(string cabinet, CancellationToken cancellationToken = default);

    public Task UpdateAsync(Classroom classroom, CancellationToken cancellationToken = default);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default);

    public Task RestoreAsync(int id, CancellationToken cancellationToken = default);
}