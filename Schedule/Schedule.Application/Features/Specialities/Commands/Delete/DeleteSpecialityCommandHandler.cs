using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Specialities.Commands.Delete;

public sealed class DeleteSpecialityCommandHandler(IScheduleDbContext context)
    : IRequestHandler<DeleteSpecialityCommand, Unit>
{
    public async Task<Unit> Handle(DeleteSpecialityCommand request, CancellationToken cancellationToken)
    {
        var speciality = await context.Specialities
            .FirstOrDefaultAsync(e => e.SpecialityId == request.Id, cancellationToken);

        if (speciality is null)
            throw new NotFoundException(nameof(Speciality), request.Id);

        speciality.IsDeleted = true;
        
        context.Specialities.Update(speciality);
        await context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}