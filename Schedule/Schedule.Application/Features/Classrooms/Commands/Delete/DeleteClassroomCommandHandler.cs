using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Classrooms.Commands.Delete;

public sealed class DeleteClassroomCommandHandler : IRequestHandler<DeleteClassroomCommand, Unit>
{
    private readonly IScheduleDbContext _context;

    public DeleteClassroomCommandHandler(IScheduleDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteClassroomCommand request, CancellationToken cancellationToken)
    {
        var classroom = await _context.Set<Classroom>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.ClassroomId == request.Id, cancellationToken);

        if (classroom is null)
            throw new NotFoundException(nameof(Classroom), request.Id);

        _context.Set<Classroom>().Remove(classroom);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}