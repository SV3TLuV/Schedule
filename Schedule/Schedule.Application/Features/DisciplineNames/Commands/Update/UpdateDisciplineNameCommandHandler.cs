using AutoMapper;
using MediatR;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.DisciplineNames.Commands.Update;

public sealed class UpdateDisciplineNameCommandHandler(
    IDisciplineNameRepository disciplineNameRepository,
    IMapper mapper) : IRequestHandler<UpdateDisciplineNameCommand, Unit>
{
    public async Task<Unit> Handle(UpdateDisciplineNameCommand request,
        CancellationToken cancellationToken)
    {
        var disciplineName = mapper.Map<DisciplineName>(request);
        await disciplineNameRepository.UpdateAsync(disciplineName, cancellationToken);
        return Unit.Value;
    }
}