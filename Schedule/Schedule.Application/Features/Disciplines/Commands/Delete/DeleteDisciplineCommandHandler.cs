using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Disciplines.Commands.Delete;

public sealed class DeleteDisciplineCommandHandler(IScheduleDbContext context)
    : IRequestHandler<DeleteDisciplineCommand, Unit>
{
    public async Task<Unit> Handle(DeleteDisciplineCommand request, CancellationToken cancellationToken)
    {
        var discipline = await context.Disciplines
            .FirstOrDefaultAsync(e => e.DisciplineId == request.Id, cancellationToken);

        if (discipline is null)
            throw new NotFoundException(nameof(Discipline), request.Id);

        discipline.IsDeleted = true;
        
        context.Disciplines.Update(discipline);
        await context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}