using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Users.Commands.Delete;

[SignalRNotification(typeof(User), CommandTypes.Delete)]
public sealed record DeleteUserCommand(int Id) : IRequest<Unit>;