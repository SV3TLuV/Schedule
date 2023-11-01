using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Teachers.Commands.Delete;

public sealed class DeleteTeacherCommandHandler : IRequestHandler<DeleteTeacherCommand, Unit>
{
    private readonly IScheduleDbContext _context;

    public DeleteTeacherCommandHandler(IScheduleDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteTeacherCommand request,
        CancellationToken cancellationToken)
    {
        var teacher = await _context.Set<Teacher>()
            .FirstOrDefaultAsync(e => e.TeacherId == request.Id, cancellationToken);

        if (teacher is null)
            throw new NotFoundException(nameof(Teacher), request.Id);

        teacher.IsDeleted = true;
        
        _context.Set<Teacher>().Update(teacher);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}