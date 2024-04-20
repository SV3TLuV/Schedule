using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Disciplines.Commands.Update;

public sealed class UpdateDisciplineCommandHandler(
    IDisciplineRepository disciplineRepository,
    IMapper mapper) : IRequestHandler<UpdateDisciplineCommand, Unit>
{
    public async Task<Unit> Handle(UpdateDisciplineCommand request, CancellationToken cancellationToken)
    {
        var discipline = mapper.Map<Discipline>(request);
        await disciplineRepository.UpdateAsync(discipline, cancellationToken);
        return Unit.Value;
    }
}