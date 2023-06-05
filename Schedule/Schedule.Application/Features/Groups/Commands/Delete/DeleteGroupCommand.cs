using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Commands.Delete;

[SignalRNotification(typeof(Group), CommandTypes.Delete)]
public sealed record DeleteGroupCommand(int Id) : IRequest<Unit>;