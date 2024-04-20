using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Disciplines.Commands.Restore;

public sealed class RestoreDisciplineCommandHandler(IDisciplineRepository disciplineRepository)
    : IRequestHandler<RestoreDisciplineCommand, Unit>
{
    public async Task<Unit> Handle(RestoreDisciplineCommand request, CancellationToken cancellationToken)
    {
        await disciplineRepository.RestoreAsync(request.Id, cancellationToken);
        return Unit.Value;
    }
}