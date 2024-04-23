using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Enums;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class EmployeeRepository : Repository, IEmployeeRepository
{
    private readonly IAccountRepository _accountRepository;

    public EmployeeRepository(IScheduleDbContext context,
        IAccountRepository accountRepository) : base(context)
    {
        accountRepository.UseContext(context);
        _accountRepository = accountRepository;
    }

    public async Task<int> CreateAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        var id = default(int);

        await Context.WithTransactionAsync(async () =>
        {
            employee.Account.RoleId = (int)AccountRole.Employee;

            var accountId = await _accountRepository.CreateAsync(employee.Account, cancellationToken);

            var created = await Context.Employees.AddAsync(new Employee
            {
                AccountId = accountId,
            }, cancellationToken);

            id = created.Entity.EmployeeId;

            foreach (var employeePermission in employee.EmployeePermissions)
            {
                await Context.EmployeePermissions.AddAsync(new EmployeePermission
                {
                    EmployeeId = id,
                    PermissionId = employeePermission.PermissionId,
                }, cancellationToken);
            }

            await Context.SaveChangesAsync(cancellationToken);
        }, cancellationToken);

        return id;
    }

    public async Task UpdateAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        await Context.WithTransactionAsync(async () =>
        {
            var employeeDb = await Context.Employees.FirstOrDefaultAsync(e =>
                e.EmployeeId == employee.EmployeeId, cancellationToken);

            if (employeeDb is null)
            {
                throw new NotFoundException(nameof(Employee), employee.EmployeeId);
            }

            employee.Account.AccountId = employeeDb.AccountId;

            await _accountRepository.UpdateAsync(employee.Account, cancellationToken);

            await Context.EmployeePermissions
                .Where(e => e.EmployeeId == employeeDb.EmployeeId)
                .ExecuteDeleteAsync(cancellationToken);

            foreach (var employeePermission in employee.EmployeePermissions)
            {
                await Context.EmployeePermissions.AddAsync(new EmployeePermission
                {
                    EmployeeId = employeeDb.EmployeeId,
                    PermissionId = employeePermission.PermissionId,
                }, cancellationToken);
            }

            await Context.SaveChangesAsync(cancellationToken);
        }, cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var employee = await Context.Employees
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.EmployeeId == id, cancellationToken);

        if (employee is null)
        {
            throw new NotFoundException(nameof(Employee), id);
        }

        await _accountRepository.DeleteAsync(employee.AccountId, cancellationToken);
    }

    public async Task RestoreAsync(int id, CancellationToken cancellationToken = default)
    {
        var employee = await Context.Employees
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.EmployeeId == id, cancellationToken);

        if (employee is null)
        {
            throw new NotFoundException(nameof(Employee), id);
        }

        await _accountRepository.RestoreAsync(employee.AccountId, cancellationToken);
    }

    public async Task UpdatePermissions(int id, int[] permissionIds, CancellationToken cancellationToken = default)
    {
        await Context.WithTransactionAsync(async () =>
        {
            var employee = await Context.Employees.FirstOrDefaultAsync(e =>
                e.EmployeeId == id, cancellationToken);

            if (employee is null)
            {
                throw new NotFoundException(nameof(Employee), id);
            }

            await Context.EmployeePermissions
                .Where(e => e.EmployeeId == employee.EmployeeId)
                .ExecuteDeleteAsync(cancellationToken);

            foreach (var permissionId in permissionIds)
            {
                await Context.EmployeePermissions.AddAsync(new EmployeePermission
                {
                    EmployeeId = id,
                    PermissionId = permissionId,
                }, cancellationToken);
            }

            await Context.SaveChangesAsync(cancellationToken);
        }, cancellationToken);
    }
}