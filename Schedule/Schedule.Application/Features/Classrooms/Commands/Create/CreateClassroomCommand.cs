using MediatR;

namespace Schedule.Application.Features.Classrooms.Commands.Create;

public sealed class CreateClassroomCommand : IRequest<int>
{
    public required string Cabinet { get; set; }
}