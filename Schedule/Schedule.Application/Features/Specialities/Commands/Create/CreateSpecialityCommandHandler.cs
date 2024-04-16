using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Specialities.Commands.Create;

public sealed class CreateSpecialityCommandHandler(IScheduleDbContext context, IMapper mapper)
    : IRequestHandler<CreateSpecialityCommand, int>
{
    public async Task<int> Handle(CreateSpecialityCommand request, CancellationToken cancellationToken)
    {
        var searched = await context.Specialities
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e =>
                e.Name == request.Name &&
                e.Code == request.Code, cancellationToken);

        if (searched is not null)
            throw new AlreadyExistsException($"Специальность: {searched.Name}");
        
        var speciality = mapper.Map<Speciality>(request);
        await context.Specialities.AddAsync(speciality, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return speciality.SpecialityId;
    }
}