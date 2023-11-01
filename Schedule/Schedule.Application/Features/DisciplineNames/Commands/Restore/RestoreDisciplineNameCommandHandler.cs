using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineNames.Commands.Restore;

public sealed class RestoreDisciplineNameCommandHandler : IRequestHandler<RestoreDisciplineNameCommand, Unit>
{
    private readonly IScheduleDbContext _context;

    public RestoreDisciplineNameCommandHandler(
        IScheduleDbContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(RestoreDisciplineNameCommand request, CancellationToken cancellationToken)
    {
        var disciplineName = await _context.Set<DisciplineName>()
            .FirstOrDefaultAsync(e => e.DisciplineNameId == request.Id, cancellationToken);

        if (disciplineName is null)
            throw new NotFoundException(nameof(DisciplineName), request.Id);

        disciplineName.IsDeleted = false;
        
        _context.Set<DisciplineName>().Update(disciplineName);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}