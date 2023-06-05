using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.TimeTypes.Commands.Create;

[SignalRNotification(typeof(TimeType), CommandTypes.Create)]
public sealed class CreateTimeTypeCommand : IRequest<int>, IMapWith<TimeType>
{
    public required string Name { get; set; }
}