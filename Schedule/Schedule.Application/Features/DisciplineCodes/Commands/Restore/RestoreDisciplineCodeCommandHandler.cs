using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineCodes.Commands.Restore;

public sealed class RestoreDisciplineCodeCommandHandler(IScheduleDbContext context)
    : IRequestHandler<RestoreDisciplineCodeCommand, Unit>
{
    public async Task<Unit> Handle(RestoreDisciplineCodeCommand request, CancellationToken cancellationToken)
    {
        var disciplineCode = await context.DisciplineCodes
            .FirstOrDefaultAsync(e => e.DisciplineCodeId == request.Id, cancellationToken);

        if (disciplineCode is null)
            throw new NotFoundException(nameof(DisciplineCode), request.Id);

        disciplineCode.IsDeleted = false;
        
        context.DisciplineCodes.Update(disciplineCode);
        await context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}