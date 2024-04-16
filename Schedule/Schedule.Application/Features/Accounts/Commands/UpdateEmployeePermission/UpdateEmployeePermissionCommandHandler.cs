using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Enums;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Accounts.Commands.UpdateEmployeePermission;

public sealed class UpdateEmployeePermissionCommandHandler(
    IScheduleDbContext context) : IRequestHandler<UpdateEmployeePermissionCommand, Unit>
{
    public async Task<Unit> Handle(UpdateEmployeePermissionCommand request, CancellationToken cancellationToken)
    {
        var account = await context.Accounts
            .AsNoTracking()
            .Include(e => e.Employees)
            .FirstOrDefaultAsync(e => e.AccountId == request.AccountId, cancellationToken);

        if (account is null)
            throw new NotFoundException(nameof(Account), request.AccountId);

        if (account.RoleId != (int)AccountRole.Admin)
            throw new NotAccessException();

        var employeePermissions = request.PermissionIds
            .Select(e => new EmployeePermission
            {
                EmployeeId = account.Employees.First().EmployeeId,
                PermissionId = 0,
            })
            .ToArray();

        await context.EmployeePermissions.AddRangeAsync(employeePermissions, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}