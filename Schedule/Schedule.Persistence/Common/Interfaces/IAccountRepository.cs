using Schedule.Core.Models;

namespace Schedule.Persistence.Common.Interfaces;

public interface IAccountRepository
{
    public Task<Account?> FindByEmail(string email, CancellationToken cancellationToken = default);
    public Task<Account?> FindByLogin(string login, CancellationToken cancellationToken = default);
}