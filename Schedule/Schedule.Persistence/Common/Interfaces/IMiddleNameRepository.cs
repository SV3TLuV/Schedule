namespace Schedule.Persistence.Common.Interfaces;

public interface IMiddleNameRepository
{
    public Task AddIfNotExist(string middleName, CancellationToken cancellationToken = default);
}