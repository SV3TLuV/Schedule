using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.DisciplineCodes.Commands.Update;

public sealed class UpdateDisciplineCodeCommandHandler(
    IDisciplineCodeRepository disciplineCodeRepository,
    IMapper mapper) : IRequestHandler<UpdateDisciplineCodeCommand, Unit>
{
    public async Task<Unit> Handle(UpdateDisciplineCodeCommand request, CancellationToken cancellationToken)
    {
        var disciplineCode = mapper.Map<DisciplineCode>(request);
        await disciplineCodeRepository.UpdateAsync(disciplineCode, cancellationToken);
        return Unit.Value;
    }
}