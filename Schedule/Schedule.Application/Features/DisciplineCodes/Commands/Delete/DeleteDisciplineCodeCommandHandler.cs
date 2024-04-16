using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineCodes.Commands.Delete;

public sealed class DeleteDisciplineCodeCommandHandler(IScheduleDbContext context)
    : IRequestHandler<DeleteDisciplineCodeCommand, Unit>
{
    public async Task<Unit> Handle(DeleteDisciplineCodeCommand request, CancellationToken cancellationToken)
    {
        var disciplineCode = await context.DisciplineCodes
            .FirstOrDefaultAsync(e => e.DisciplineCodeId == request.Id, cancellationToken);

        if (disciplineCode is null)
            throw new NotFoundException(nameof(DisciplineCode), request.Id);

        disciplineCode.IsDeleted = true;
        
        context.DisciplineCodes.Update(disciplineCode);
        await context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}