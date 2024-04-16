using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Classrooms.Commands.Delete;

public sealed class DeleteClassroomCommandHandler(IScheduleDbContext context)
    : IRequestHandler<DeleteClassroomCommand, Unit>
{
    public async Task<Unit> Handle(DeleteClassroomCommand request, CancellationToken cancellationToken)
    {
        var classroom = await context.Classrooms
            .FirstOrDefaultAsync(e => e.ClassroomId == request.Id, cancellationToken);

        if (classroom is null)
            throw new NotFoundException(nameof(Classroom), request.Id);

        classroom.IsDeleted = true;
        
        context.Classrooms.Update(classroom);
        await context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}