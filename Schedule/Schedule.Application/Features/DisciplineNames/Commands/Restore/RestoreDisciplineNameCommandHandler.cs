using MediatR;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.DisciplineNames.Commands.Restore;

public sealed class RestoreDisciplineNameCommandHandler(IDisciplineNameRepository disciplineNameRepository)
    : IRequestHandler<RestoreDisciplineNameCommand, Unit>
{
    public async Task<Unit> Handle(RestoreDisciplineNameCommand request, CancellationToken cancellationToken)
    {
        await disciplineNameRepository.RestoreAsync(request.Id, cancellationToken);
        return Unit.Value;
    }
}