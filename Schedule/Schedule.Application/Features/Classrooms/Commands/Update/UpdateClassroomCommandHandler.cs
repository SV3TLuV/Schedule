using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Classrooms.Commands.Update;

public sealed class UpdateClassroomCommandHandler(IScheduleDbContext context, IMapper mapper)
    : IRequestHandler<UpdateClassroomCommand, Unit>
{
    public async Task<Unit> Handle(UpdateClassroomCommand request, CancellationToken cancellationToken)
    {
        var classroom = await context.Classrooms
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.ClassroomId == request.Id, cancellationToken);

        if (classroom is null)
            throw new NotFoundException(nameof(Classroom), request.Id);

        classroom.Cabinet = request.Cabinet;

        context.Classrooms.Update(classroom);
        await context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}