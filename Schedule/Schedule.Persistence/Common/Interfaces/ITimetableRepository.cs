using Schedule.Core.Models;

namespace Schedule.Persistence.Common.Interfaces;

public interface ITimetableRepository
{
    public Task<int> CreateAsync(Timetable timetable, CancellationToken cancellationToken = default);
    public Task UpdateAsync(Timetable timetable, CancellationToken cancellationToken = default);

    public Task CreateForGroupAsync(int groupId, CancellationToken cancellationToken = default);
}