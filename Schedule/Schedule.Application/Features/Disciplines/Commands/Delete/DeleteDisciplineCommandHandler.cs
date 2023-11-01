using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Disciplines.Commands.Delete;

public sealed class DeleteDisciplineCommandHandler : IRequestHandler<DeleteDisciplineCommand, Unit>
{
    private readonly IScheduleDbContext _context;

    public DeleteDisciplineCommandHandler(IScheduleDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteDisciplineCommand request, CancellationToken cancellationToken)
    {
        var discipline = await _context.Set<Discipline>()
            .FirstOrDefaultAsync(e => e.DisciplineId == request.Id, cancellationToken);

        if (discipline is null)
            throw new NotFoundException(nameof(Discipline), request.Id);

        discipline.IsDeleted = true;
        
        _context.Set<Discipline>().Update(discipline);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}