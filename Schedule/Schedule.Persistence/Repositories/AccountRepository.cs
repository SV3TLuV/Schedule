using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class AccountRepository(IScheduleDbContext context) : IAccountRepository
{
    public async Task<Account?> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await context.Accounts
            .Include(e => e.Role)
            .Include(e => e.Employees)
            .Include(e => e.Teachers)
            .Include(e => e.Students)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Email == email, cancellationToken);
    }

    public async Task<Account?> FindByLoginAsync(string login, CancellationToken cancellationToken = default)
    {
        return await context.Accounts
            .Include(e => e.Role)
            .Include(e => e.Employees)
            .Include(e => e.Teachers)
            .Include(e => e.Students)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Login == login, cancellationToken);
    }
}