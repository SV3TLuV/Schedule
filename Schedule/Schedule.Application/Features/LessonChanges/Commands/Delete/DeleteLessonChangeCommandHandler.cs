using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.LessonChanges.Commands.Delete;

public sealed class DeleteLessonChangeCommandHandler(ILessonChangeRepository lessonChangeRepository)
    : IRequestHandler<DeleteLessonChangeCommand, Unit>
{
    public async Task<Unit> Handle(DeleteLessonChangeCommand request, CancellationToken cancellationToken)
    {
        await lessonChangeRepository.DeleteAsync(request.LessonChangeId, cancellationToken);
        return Unit.Value;
    }
}