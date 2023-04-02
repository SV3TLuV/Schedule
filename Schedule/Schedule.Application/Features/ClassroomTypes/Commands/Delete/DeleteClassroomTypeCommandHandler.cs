using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Persistence.Context;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.ClassroomTypes.Commands.Delete;

public sealed class DeleteClassroomTypeCommandHandler : IRequestHandler<DeleteClassroomTypeCommand>
{
    private readonly ScheduleDbContext _context;

    public DeleteClassroomTypeCommandHandler(ScheduleDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(DeleteClassroomTypeCommand request, CancellationToken cancellationToken)
    {
        var classroomType = await _context.ClassroomTypes
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.ClassroomTypeId == request.Id, cancellationToken);
        
        if (classroomType is null)
            throw new NotFoundException(nameof(ClassroomType), request.Id);

        _context.ClassroomTypes.Remove(classroomType);
        await _context.SaveChangesAsync(cancellationToken);
    }
}