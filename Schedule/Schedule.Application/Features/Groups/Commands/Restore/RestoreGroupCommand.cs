using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Commands.Restore;

[SignalRNotification(typeof(Group), CommandTypes.Restore)]
public sealed record RestoreGroupCommand(int Id) : IRequest<Unit>;