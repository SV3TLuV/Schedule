using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Persistence.Context;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.Classrooms.Commands.Delete;

public sealed class DeleteClassroomCommandHandler : IRequestHandler<DeleteClassroomCommand>
{
    private readonly ScheduleDbContext _context;

    public DeleteClassroomCommandHandler(ScheduleDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(DeleteClassroomCommand request, CancellationToken cancellationToken)
    {
        var classroom = await _context.Classrooms
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.ClassroomId == request.Id, cancellationToken);
        
        if (classroom is null)
            throw new NotFoundException(nameof(Classroom), request.Id);

        _context.Classrooms.Remove(classroom);
        await _context.SaveChangesAsync(cancellationToken);
    }
}