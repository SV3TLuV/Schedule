using MediatR;

namespace Schedule.Application.Features.Accounts.Commands.UpdateEmployeePermission;

public sealed class UpdateEmployeePermissionCommand : IRequest<Unit>
{
    public int AccountId { get; set; }
    public required ICollection<int> PermissionIds { get; set; }
}