using MediatR;

namespace Schedule.Application.Features.Teachers.Commands.Delete;

public sealed class DeleteTeacherCommand : IRequest<Unit>
{
    public int Id { get; set; }
}