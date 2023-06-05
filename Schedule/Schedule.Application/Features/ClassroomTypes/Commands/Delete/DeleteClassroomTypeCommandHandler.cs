using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.ClassroomTypes.Commands.Delete;

public sealed class DeleteClassroomTypeCommandHandler : IRequestHandler<DeleteClassroomTypeCommand, Unit>
{
    private readonly IScheduleDbContext _context;

    public DeleteClassroomTypeCommandHandler(IScheduleDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteClassroomTypeCommand request, CancellationToken cancellationToken)
    {
        var classroomType = await _context.Set<ClassroomType>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.ClassroomTypeId == request.Id, cancellationToken);

        if (classroomType is null)
            throw new NotFoundException(nameof(ClassroomType), request.Id);

        _context.Set<ClassroomType>().Remove(classroomType);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}