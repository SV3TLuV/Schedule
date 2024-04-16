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
        var classroomDbo = await context.Classrooms
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.ClassroomId == request.Id, cancellationToken);

        if (classroomDbo is null)
            throw new NotFoundException(nameof(Classroom), request.Id);

        var classroom = mapper.Map<Classroom>(request);
        context.Classrooms.Update(classroom);
        await context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}