namespace Schedule.Persistence.Common.Interfaces;

public interface ISurnameRepository
{
    public Task AddIfNotExist(string surname, CancellationToken cancellationToken = default);
}