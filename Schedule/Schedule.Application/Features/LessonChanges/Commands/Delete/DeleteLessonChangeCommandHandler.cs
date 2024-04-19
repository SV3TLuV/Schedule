using MediatR;
using Schedule.Core.Common.Interfaces;

namespace Schedule.Application.Features.LessonChanges.Commands.Delete;

public sealed class DeleteLessonChangeCommandHandler(
    IScheduleDbContext context) : IRequestHandler<DeleteLessonChangeCommand, Unit>
{
    public async Task<Unit> Handle(DeleteLessonChangeCommand request, CancellationToken cancellationToken)
    {
        

        return Unit.Value;
    }
}