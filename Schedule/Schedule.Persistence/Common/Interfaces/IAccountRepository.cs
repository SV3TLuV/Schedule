using Schedule.Core.Models;

namespace Schedule.Persistence.Common.Interfaces;

public interface IAccountRepository
{
    public Task<Account?> FindByEmailAsync(string email, CancellationToken cancellationToken = default);
    public Task<Account?> FindByLoginAsync(string login, CancellationToken cancellationToken = default);
}