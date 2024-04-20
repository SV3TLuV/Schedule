using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.DisciplineCodes.Commands.Delete;

public sealed class DeleteDisciplineCodeCommandHandler(IDisciplineCodeRepository disciplineCodeRepository)
    : IRequestHandler<DeleteDisciplineCodeCommand, Unit>
{
    public async Task<Unit> Handle(DeleteDisciplineCodeCommand request, CancellationToken cancellationToken)
    {
        await disciplineCodeRepository.DeleteAsync(request.Id, cancellationToken);
        return Unit.Value;
    }
}