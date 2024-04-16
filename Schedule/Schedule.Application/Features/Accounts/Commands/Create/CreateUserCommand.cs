using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Users.Commands.Create;

public sealed class CreateUserCommand : IRequest<int>, IMapWith<User>
{
    public required string Login { get; set; }

    public required string Password { get; set; }

    public int RoleId { get; set; }
}