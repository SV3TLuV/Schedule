using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Disciplines.Commands.Create;

public sealed class CreateDisciplineCommandHandler(
    IDisciplineRepository disciplineRepository,
    IMapper mapper) : IRequestHandler<CreateDisciplineCommand, int>
{
    public async Task<int> Handle(CreateDisciplineCommand request, CancellationToken cancellationToken)
    {
        var discipline = mapper.Map<Discipline>(request);
        return await disciplineRepository.CreateAsync(discipline, cancellationToken);
    }
}