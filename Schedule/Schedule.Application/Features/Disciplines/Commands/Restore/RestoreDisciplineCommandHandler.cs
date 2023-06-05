using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Disciplines.Commands.Restore;

public sealed class RestoreDisciplineCommandHandler : IRequestHandler<RestoreDisciplineCommand, Unit>
{
    private readonly IScheduleDbContext _context;

    public RestoreDisciplineCommandHandler(
        IScheduleDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(RestoreDisciplineCommand request, CancellationToken cancellationToken)
    {
        var discipline = await _context.Set<Discipline>()
            .FirstOrDefaultAsync(e => e.DisciplineId == request.Id, cancellationToken);

        if (discipline is null)
            throw new NotFoundException(nameof(Discipline), request.Id);

        discipline.IsDeleted = false;
        _context.Set<Discipline>().Update(discipline);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}