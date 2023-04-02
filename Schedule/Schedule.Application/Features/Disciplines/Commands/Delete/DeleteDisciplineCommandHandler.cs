using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Persistence.Context;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.Disciplines.Commands.Delete;

public sealed class DeleteDisciplineCommandHandler : IRequestHandler<DeleteDisciplineCommand>
{
    private readonly ScheduleDbContext _context;

    public DeleteDisciplineCommandHandler(ScheduleDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(DeleteDisciplineCommand request, CancellationToken cancellationToken)
    {
        var discipline = await _context.Disciplines
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.DisciplineId == request.Id, cancellationToken);
        
        if (discipline is null)
            throw new NotFoundException(nameof(Discipline), request.Id);

        _context.Disciplines.Remove(discipline);
        await _context.SaveChangesAsync(cancellationToken);
    }
}