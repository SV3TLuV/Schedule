namespace Schedule.Persistence.Common.Interfaces;

public interface INameRepository
{
    public Task AddIfNotExist(string name, CancellationToken cancellationToken = default);
}