using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class AccountRepository : Repository, IAccountRepository
{
    private readonly INameRepository _nameRepository;
    private readonly ISurnameRepository _surnameRepository;
    private readonly IMiddleNameRepository _middleNameRepository;
    private readonly IPasswordHasherService _passwordHasherService;

    public AccountRepository(
        IScheduleDbContext context,
        INameRepository nameRepository,
        ISurnameRepository surnameRepository,
        IMiddleNameRepository middleNameRepository,
        IPasswordHasherService passwordHasherService) : base(context)
    {
        nameRepository.UseContext(context);
        surnameRepository.UseContext(context);
        middleNameRepository.UseContext(context);

        _nameRepository = nameRepository;
        _surnameRepository = surnameRepository;
        _middleNameRepository = middleNameRepository;
        _passwordHasherService = passwordHasherService;
    }


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

        await _nameRepository.AddIfNotExistAsync(account.Name, cancellationToken);
        await _surnameRepository.AddIfNotExistAsync(account.Surname, cancellationToken);

        if (account.MiddleName is not null)
        {
            await _middleNameRepository.AddIfNotExistAsync(account.MiddleName, cancellationToken);
        }

        account.PasswordHash = _passwordHasherService.Hash(account.PasswordHash);

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

        await _nameRepository.AddIfNotExistAsync(account.Name, cancellationToken);
        await _surnameRepository.AddIfNotExistAsync(account.Surname, cancellationToken);

        if (account.MiddleName is not null)
        {
            await _middleNameRepository.AddIfNotExistAsync(account.MiddleName, cancellationToken);
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
        await Context.WithTransactionAsync(async () =>
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

        account.PasswordHash = _passwordHasherService.Hash(account.PasswordHash);

        Context.Accounts.Update(account);
        await Context.SaveChangesAsync(cancellationToken);
    }
}