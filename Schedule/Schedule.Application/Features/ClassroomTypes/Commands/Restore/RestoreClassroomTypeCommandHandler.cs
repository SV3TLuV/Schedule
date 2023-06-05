using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.ClassroomTypes.Commands.Restore;

public sealed class RestoreClassroomTypeCommandHandler : IRequestHandler<RestoreClassroomTypeCommand, Unit>
{
    private readonly IScheduleDbContext _context;

    public RestoreClassroomTypeCommandHandler(
        IScheduleDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(RestoreClassroomTypeCommand request, CancellationToken cancellationToken)
    {
        var classroomType = await _context.Set<ClassroomType>()
            .FirstOrDefaultAsync(e => e.ClassroomTypeId == request.Id, cancellationToken);

        if (classroomType is null)
            throw new NotFoundException(nameof(ClassroomType), request.Id);

        classroomType.IsDeleted = false;
        _context.Set<ClassroomType>().Update(classroomType);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}