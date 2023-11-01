using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineNames.Commands.Delete;

public sealed class DeleteDisciplineNameCommandHandler : IRequestHandler<DeleteDisciplineNameCommand, Unit>
{
    private readonly IScheduleDbContext _context;

    public DeleteDisciplineNameCommandHandler(
        IScheduleDbContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(DeleteDisciplineNameCommand request, CancellationToken cancellationToken)
    {
        var disciplineName = await _context.Set<DisciplineName>()
            .FirstOrDefaultAsync(e => e.DisciplineNameId == request.Id, cancellationToken);

        if (disciplineName is null)
            throw new NotFoundException(nameof(DisciplineName), request.Id);

        disciplineName.IsDeleted = true;
        
        _context.Set<DisciplineName>().Update(disciplineName);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}