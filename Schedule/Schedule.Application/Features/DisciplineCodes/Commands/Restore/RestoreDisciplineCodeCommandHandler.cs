using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineCodes.Commands.Restore;

public sealed class RestoreDisciplineCodeCommandHandler : IRequestHandler<RestoreDisciplineCodeCommand, Unit>
{
    private readonly IScheduleDbContext _context;

    public RestoreDisciplineCodeCommandHandler(
        IScheduleDbContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(RestoreDisciplineCodeCommand request, CancellationToken cancellationToken)
    {
        var disciplineCode = await _context.Set<DisciplineCode>()
            .FirstOrDefaultAsync(e => e.DisciplineCodeId == request.Id, cancellationToken);

        if (disciplineCode is null)
            throw new NotFoundException(nameof(DisciplineCode), request.Id);

        disciplineCode.IsDeleted = false;
        
        _context.Set<DisciplineCode>().Update(disciplineCode);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}