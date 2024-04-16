using MediatR;

namespace Schedule.Application.Features.Accounts.Commands.ChangePassword;

public sealed class ChangePasswordCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public required string Password { get; set; }
}