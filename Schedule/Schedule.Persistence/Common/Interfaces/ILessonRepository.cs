using Schedule.Core.Models;

namespace Schedule.Persistence.Common.Interfaces;

public interface ILessonRepository : IRepository
{
    public Task<int> CreateAsync(Lesson lessonChange, CancellationToken cancellationToken = default);
    public Task UpdateAsync(Lesson lesson, CancellationToken cancellationToken = default);
}