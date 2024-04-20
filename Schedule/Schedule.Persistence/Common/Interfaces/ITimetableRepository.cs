using Schedule.Core.Models;

namespace Schedule.Persistence.Common.Interfaces;

public interface ITimetableRepository : IRepository
{
    public Task<int> CreateAsync(Timetable timetable, CancellationToken cancellationToken = default);
    public Task UpdateEndedAsync(Timetable timetable, CancellationToken cancellationToken = default);
}