using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Models;

namespace Schedule.Application.Features.TimeTypes.Commands.Delete;

[SignalRNotification(typeof(TimeType), CommandTypes.Delete)]
public sealed record DeleteTimeTypeCommand(int Id) : IRequest<Unit>;