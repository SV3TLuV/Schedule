using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Disciplines.Commands.Delete;

public sealed class DeleteDisciplineCommandHandler : IRequestHandler<DeleteDisciplineCommand>
{
    private readonly IScheduleDbContext _context;

    public DeleteDisciplineCommandHandler(IScheduleDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteDisciplineCommand request, CancellationToken cancellationToken)
    {
        var discipline = await _context.Set<Discipline>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.DisciplineId == request.Id, cancellationToken);

        if (discipline is null)
            throw new NotFoundException(nameof(Discipline), request.Id);

        _context.Set<Discipline>().Remove(discipline);
        await _context.SaveChangesAsync(cancellationToken);
    }
}