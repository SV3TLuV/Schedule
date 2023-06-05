using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Models;

namespace Schedule.Application.Features.TimeTypes.Commands.Restore;

[SignalRNotification(typeof(TimeType), CommandTypes.Restore)]
public sealed record RestoreTimeTypeCommand(int Id) : IRequest<Unit>;