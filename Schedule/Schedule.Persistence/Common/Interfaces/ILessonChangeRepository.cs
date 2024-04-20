using Schedule.Core.Models;

namespace Schedule.Persistence.Common.Interfaces;

public interface ILessonChangeRepository : IRepository
{
    public Task<int> CreateAsync(LessonChange lessonChange, CancellationToken cancellationToken = default);
    public Task UpdateAsync(LessonChange lessonChange, CancellationToken cancellationToken = default);
    public Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}