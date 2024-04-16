using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineNames.Commands.Delete;

public sealed class DeleteDisciplineNameCommandHandler(IScheduleDbContext context)
    : IRequestHandler<DeleteDisciplineNameCommand, Unit>
{
    public async Task<Unit> Handle(DeleteDisciplineNameCommand request, CancellationToken cancellationToken)
    {
        var disciplineName = await context.DisciplineNames
            .FirstOrDefaultAsync(e => e.DisciplineNameId == request.Id, cancellationToken);

        if (disciplineName is null)
            throw new NotFoundException(nameof(DisciplineName), request.Id);

        disciplineName.IsDeleted = true;
        
        context.DisciplineNames.Update(disciplineName);
        await context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}