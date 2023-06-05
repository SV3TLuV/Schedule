using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Times.Commands.Restore;

[SignalRNotification(typeof(Time), CommandTypes.Restore)]
public sealed record RestoreTimeCommand(int Id) : IRequest<Unit>;