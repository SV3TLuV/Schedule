using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Classrooms.Commands.Restore;

public sealed class RestoreClassroomCommandHandler(IScheduleDbContext context)
    : IRequestHandler<RestoreClassroomCommand, Unit>
{
    public async Task<Unit> Handle(RestoreClassroomCommand request,
        CancellationToken cancellationToken)
    {
        var classroom = await context.Classrooms
            .FirstOrDefaultAsync(e => e.ClassroomId == request.Id, cancellationToken);

        if (classroom is null)
            throw new NotFoundException(nameof(Classroom), request.Id);

        classroom.IsDeleted = false;
        context.Classrooms.Update(classroom);
        await context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}