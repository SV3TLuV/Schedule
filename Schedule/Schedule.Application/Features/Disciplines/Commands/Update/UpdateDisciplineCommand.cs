using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.Disciplines.Commands.Update;

public sealed class UpdateDisciplineCommand : IRequest, IMapWith<Discipline>
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Code { get; set; }
}