namespace Schedule.Persistence.Common.Interfaces;

public interface INameRepository : IRepository
{
    public Task AddIfNotExistAsync(string name, CancellationToken cancellationToken = default);
}