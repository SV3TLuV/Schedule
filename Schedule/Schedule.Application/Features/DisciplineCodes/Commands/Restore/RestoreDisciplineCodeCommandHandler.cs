using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.DisciplineCodes.Commands.Restore;

public sealed class RestoreDisciplineCodeCommandHandler(IDisciplineCodeRepository disciplineCodeRepository)
    : IRequestHandler<RestoreDisciplineCodeCommand, Unit>
{
    public async Task<Unit> Handle(RestoreDisciplineCodeCommand request, CancellationToken cancellationToken)
    {
        await disciplineCodeRepository.RestoreAsync(request.Id, cancellationToken);
        return Unit.Value;
    }
}