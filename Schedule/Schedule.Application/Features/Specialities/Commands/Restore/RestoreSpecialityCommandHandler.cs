using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Specialities.Commands.Restore;

public sealed class RestoreSpecialityCommandHandler(IScheduleDbContext context)
    : IRequestHandler<RestoreSpecialityCommand, Unit>
{
    public async Task<Unit> Handle(RestoreSpecialityCommand request, CancellationToken cancellationToken)
    {
        var speciality = await context.Specialities
            .FirstOrDefaultAsync(e => e.SpecialityId == request.Id, cancellationToken);

        if (speciality is null)
            throw new NotFoundException(nameof(Speciality), request.Id);

        speciality.IsDeleted = false;
        context.Specialities.Update(speciality);
        await context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}