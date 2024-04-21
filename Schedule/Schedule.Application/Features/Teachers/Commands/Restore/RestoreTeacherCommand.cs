using MediatR;

namespace Schedule.Application.Features.Teachers.Commands.Restore;

public sealed class RestoreTeacherCommand : IRequest<Unit>
{
    public int Id { get; set; }
}