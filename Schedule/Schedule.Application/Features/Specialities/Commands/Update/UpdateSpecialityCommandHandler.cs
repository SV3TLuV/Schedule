using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Specialities.Commands.Update;

public sealed class UpdateSpecialityCommandHandler(
    ISpecialityRepository specialityRepository,
    IMapper mapper)
    : IRequestHandler<UpdateSpecialityCommand, Unit>
{
    public async Task<Unit> Handle(UpdateSpecialityCommand request, CancellationToken cancellationToken)
    {
        var speciality = mapper.Map<Speciality>(request);
        await specialityRepository.UpdateAsync(speciality, cancellationToken);
        return Unit.Value;
    }
}