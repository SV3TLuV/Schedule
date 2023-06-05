using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Times.Commands.Delete;

[SignalRNotification(typeof(Time), CommandTypes.Delete)]
public sealed record DeleteTimeCommand(int Id) : IRequest<Unit>;