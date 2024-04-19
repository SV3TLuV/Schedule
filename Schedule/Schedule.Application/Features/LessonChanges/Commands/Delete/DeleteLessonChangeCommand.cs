using MediatR;

namespace Schedule.Application.Features.LessonChanges.Commands.Delete;

public sealed class DeleteLessonChangeCommand : IRequest<Unit>
{
    public int LessonChangeId { get; set; }
}