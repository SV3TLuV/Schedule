namespace Schedule.Persistence.Common.Interfaces;

public interface IDbInitializer
{
    Task InitializeAsync(CancellationToken cancellationToken = default);
}