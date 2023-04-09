using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Disciplines.Commands.Create;

public sealed class CreateDisciplineCommand : IRequest<int>, IMapWith<Discipline>
{
    public required string Name { get; set; }
    public required string Code { get; set; }
}