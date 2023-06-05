using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Users.Commands.Create;

[SignalRNotification(typeof(User), CommandTypes.Create)]
public sealed class CreateUserCommand : IRequest<int>, IMapWith<User>
{
    public required string Login { get; set; }

    public required string Password { get; set; }

    public int RoleId { get; set; }
}