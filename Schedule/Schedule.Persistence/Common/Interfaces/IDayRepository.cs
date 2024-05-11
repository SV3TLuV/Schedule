using Schedule.Core.Models;

namespace Schedule.Persistence.Common.Interfaces;

public interface IDayRepository
{
    public Task UpdateAsync(Day day, CancellationToken cancellationToken = default);
}