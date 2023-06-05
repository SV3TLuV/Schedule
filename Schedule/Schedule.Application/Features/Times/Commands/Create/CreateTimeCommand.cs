using MediatR;
using Schedule.Application.Common.Attributes;
using Schedule.Application.Features.Base;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Times.Commands.Create;

[SignalRNotification(typeof(Time), CommandTypes.Create)]
public sealed class CreateTimeCommand : IRequest<int>, IMapWith<Time>
{
    public required string Start { get; set; }
    public required string End { get; set; }
    public required int LessonNumber { get; set; }
    public required int Duration { get; set; }
    public required int TypeId { get; set; }
}