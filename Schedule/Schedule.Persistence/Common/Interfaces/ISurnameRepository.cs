namespace Schedule.Persistence.Common.Interfaces;

public interface ISurnameRepository : IRepository
{
    public Task AddIfNotExistAsync(string surname, CancellationToken cancellationToken = default);
}