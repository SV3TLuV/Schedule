using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineCodes.Commands.Delete;

public sealed class DeleteDisciplineCodeCommandHandler : IRequestHandler<DeleteDisciplineCodeCommand, Unit>
{
    private readonly IScheduleDbContext _context;

    public DeleteDisciplineCodeCommandHandler(IScheduleDbContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(DeleteDisciplineCodeCommand request, CancellationToken cancellationToken)
    {
        var disciplineCode = await _context.Set<DisciplineCode>()
            .FirstOrDefaultAsync(e => e.DisciplineCodeId == request.Id, cancellationToken);

        if (disciplineCode is null)
            throw new NotFoundException(nameof(DisciplineCode), request.Id);

        disciplineCode.IsDeleted = true;
        
        _context.Set<DisciplineCode>().Update(disciplineCode);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}