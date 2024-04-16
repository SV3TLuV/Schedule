using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Specialities.Commands.Update;

public sealed class UpdateSpecialityCommandHandler(IScheduleDbContext context, IMapper mapper)
    : IRequestHandler<UpdateSpecialityCommand, Unit>
{
    public async Task<Unit> Handle(UpdateSpecialityCommand request, CancellationToken cancellationToken)
    {
        var specialityDbo = await context.Specialities
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.SpecialityId == request.Id, cancellationToken);

        if (specialityDbo is null)
            throw new NotFoundException(nameof(Speciality), request.Id);
        
        var speciality = mapper.Map<Speciality>(request);
        context.Specialities.Update(speciality);
        await context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}