using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.TimeTypes.Commands.Create;

public sealed class CreateTimeTypeCommand : IRequest<int>, IMapWith<TimeType>
{
    public required string Name { get; set; }
}