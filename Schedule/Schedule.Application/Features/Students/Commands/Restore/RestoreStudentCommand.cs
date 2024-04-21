using MediatR;

namespace Schedule.Application.Features.Students.Commands.Restore;

public sealed class RestoreStudentCommand : IRequest<Unit>
{
    public int Id { get; set; }
}