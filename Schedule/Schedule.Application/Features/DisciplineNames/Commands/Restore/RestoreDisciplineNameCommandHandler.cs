using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineNames.Commands.Restore;

public sealed class RestoreDisciplineNameCommandHandler(IScheduleDbContext context)
    : IRequestHandler<RestoreDisciplineNameCommand, Unit>
{
    public async Task<Unit> Handle(RestoreDisciplineNameCommand request, CancellationToken cancellationToken)
    {
        var disciplineName = await context.DisciplineNames
            .FirstOrDefaultAsync(e => e.DisciplineNameId == request.Id, cancellationToken);

        if (disciplineName is null)
            throw new NotFoundException(nameof(DisciplineName), request.Id);

        disciplineName.IsDeleted = false;
        
        context.DisciplineNames.Update(disciplineName);
        await context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}