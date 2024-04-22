using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Specialities.Commands.Restore;

public sealed class RestoreSpecialityCommandHandler(ISpecialityRepository specialityRepository)
    : IRequestHandler<RestoreSpecialityCommand, Unit>
{
    public async Task<Unit> Handle(RestoreSpecialityCommand request, CancellationToken cancellationToken)
    {
        await specialityRepository.RestoreAsync(request.Id, cancellationToken);
        return Unit.Value;
    }
}