using MediatR;

namespace Schedule.Application.Features.Students.Commands.Delete;

public sealed class DeleteStudentCommand : IRequest<Unit>
{
    public int Id { get; set; }
}