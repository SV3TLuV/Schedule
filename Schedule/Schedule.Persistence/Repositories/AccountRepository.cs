using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class AccountRepository(
    IScheduleDbContext context,
    IPasswordHasherService passwordHasherService) : Repository(context), IAccountRepository
{
    public async Task<int> CreateAsync(Account account, CancellationToken cancellationToken = default)
    {
        var searchByLogin = await FindByLoginAsync(account.Login, cancellationToken);

        if (searchByLogin is not null)
        {
            throw new AlreadyExistsException(account.Login);
        }

        var searchByEmail = await FindByEmailAsync(account.Email, cancellationToken);

        if (searchByEmail is not null)
        {
            throw new AlreadyExistsException(account.Email);
        }

        account.PasswordHash = passwordHasherService.Hash(account.PasswordHash);

        var created = await Context.Accounts.AddAsync(account, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
        return created.Entity.AccountId;
    }

    public async Task UpdateAsync(Account account, CancellationToken cancellationToken = default)
    {
        var accountDb = await Context.Accounts.FirstOrDefaultAsync(e =>
            e.AccountId == account.AccountId, cancellationToken);

        if (accountDb is null)
        {
            throw new NotFoundException(nameof(Account), account.AccountId);
        }

        var searchByLogin = await FindByLoginAsync(account.Login, cancellationToken);

        if (searchByLogin is not null)
        {
            throw new AlreadyExistsException(account.Login);
        }

        var searchByEmail = await FindByEmailAsync(account.Email, cancellationToken);

        if (searchByEmail is not null)
        {
            throw new AlreadyExistsException(account.Email);
        }

        accountDb.Name = account.Name;
        accountDb.Surname = account.Surname;
        accountDb.MiddleName = account.MiddleName;
        accountDb.Email = account.Email;
        accountDb.RoleId = account.RoleId;

        Context.Accounts.Update(accountDb);
        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int accountId, CancellationToken cancellationToken = default)
    {
        await context.WithTransactionAsync(async () =>
        {
            var account = await Context.Accounts.FirstOrDefaultAsync(e =>
                e.AccountId == accountId, cancellationToken);

            if (account is null)
            {
                throw new NotFoundException(nameof(Account), accountId);
            }

            account.IsDeleted = true;

            Context.Accounts.Update(account);

            await Context.Sessions
                .Where(e => e.AccountId == account.AccountId)
                .ExecuteDeleteAsync(cancellationToken);

            await Context.SaveChangesAsync(cancellationToken);
        }, cancellationToken);
    }

    public async Task RestoreAsync(int accountId, CancellationToken cancellationToken = default)
    {
        var account = await Context.Accounts.FirstOrDefaultAsync(e =>
            e.AccountId == accountId, cancellationToken);

        if (account is null)
        {
            throw new NotFoundException(nameof(Account), accountId);
        }

        account.IsDeleted = false;

        Context.Accounts.Update(account);
        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Account?> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await Context.Accounts
            .Include(e => e.Role)
            .Include(e => e.Employees)
            .Include(e => e.Teachers)
            .Include(e => e.Students)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Email == email, cancellationToken);
    }

    public async Task<Account?> FindByLoginAsync(string login, CancellationToken cancellationToken = default)
    {
        return await Context.Accounts
            .Include(e => e.Role)
            .Include(e => e.Employees)
            .Include(e => e.Teachers)
            .Include(e => e.Students)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Login == login, cancellationToken);
    }

    public async Task ChangePasswordAsync(int accountId, string password, CancellationToken cancellationToken = default)
    {
        var account = await Context.Accounts.FirstOrDefaultAsync(e =>
            e.AccountId == accountId, cancellationToken);

        if (account is null)
        {
            throw new NotFoundException(nameof(Account), accountId);
        }

        account.PasswordHash = passwordHasherService.Hash(account.PasswordHash);

        Context.Accounts.Update(account);
        await Context.SaveChangesAsync(cancellationToken);
    }
}