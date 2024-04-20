using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Specialities.Commands.Create;

public sealed class CreateSpecialityCommandHandler(
    IMapper mapper,
    ISpecialityRepository specialityRepository)
    : IRequestHandler<CreateSpecialityCommand, int>
{
    public async Task<int> Handle(CreateSpecialityCommand request, CancellationToken cancellationToken)
    {
        var speciality = mapper.Map<Speciality>(request);
        return await specialityRepository.CreateAsync(speciality, cancellationToken);
    }
}