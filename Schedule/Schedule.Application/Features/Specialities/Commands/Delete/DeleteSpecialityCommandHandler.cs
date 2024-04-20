using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Specialities.Commands.Delete;

public sealed class DeleteSpecialityCommandHandler(ISpecialityRepository specialityRepository)
    : IRequestHandler<DeleteSpecialityCommand, Unit>
{
    public async Task<Unit> Handle(DeleteSpecialityCommand request, CancellationToken cancellationToken)
    {
        await specialityRepository.DeleteAsync(request.Id, cancellationToken);
        return Unit.Value;
    }
}