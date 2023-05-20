using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Classrooms.Commands.Restore;

public sealed class RestoreClassroomCommandHandler : IRequestHandler<RestoreClassroomCommand>
{
    private readonly IScheduleDbContext _context;

    public RestoreClassroomCommandHandler(
        IScheduleDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(RestoreClassroomCommand request, 
        CancellationToken cancellationToken)
    {
        var classroom = await _context.Set<Classroom>()
            .FirstOrDefaultAsync(e => e.ClassroomId == request.Id, cancellationToken);

        if (classroom is null)
            throw new NotFoundException(nameof(Classroom), request.Id);

        classroom.IsDeleted = false;
        _context.Set<Classroom>().Update(classroom);
        await _context.SaveChangesAsync(cancellationToken);
    }
}