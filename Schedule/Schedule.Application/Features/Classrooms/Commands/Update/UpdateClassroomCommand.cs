using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.Classrooms.Commands.Update;

public sealed class UpdateClassroomCommand : IRequest, IMapWith<Classroom>
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required int[] TypeIds { get; set; }
}