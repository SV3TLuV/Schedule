using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.Classrooms.Commands.Create;

public sealed class CreateClassroomCommand : IRequest<int>, IMapWith<Classroom>
{
    public required string Cabinet { get; set; }
    public required int[] TypeIds { get; set; }
}