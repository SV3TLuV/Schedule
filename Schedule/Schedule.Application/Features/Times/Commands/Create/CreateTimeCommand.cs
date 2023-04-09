using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Times.Commands.Create;

public sealed class CreateTimeCommand : IRequest<int>, IMapWith<Time>
{
    public required TimeSpan Start { get; set; }
    public required TimeSpan End { get; set; }
    public required int LessonNumber { get; set; }
    public required int TypeId { get; set; }
}