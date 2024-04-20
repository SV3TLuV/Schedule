using MediatR;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.DisciplineNames.Commands.Create;

public sealed class CreateDisciplineNameCommandHandler(IDisciplineNameRepository disciplineNameRepository)
    : IRequestHandler<CreateDisciplineNameCommand, int>
{
    public async Task<int> Handle(CreateDisciplineNameCommand request,
        CancellationToken cancellationToken)
    {
        return await disciplineNameRepository.AddIfNotExistAsync(request.Name, cancellationToken);
    }
}