using MediatR;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.DisciplineNames.Commands.Delete;

public sealed class DeleteDisciplineNameCommandHandler(IDisciplineNameRepository disciplineNameRepository)
    : IRequestHandler<DeleteDisciplineNameCommand, Unit>
{
    public async Task<Unit> Handle(DeleteDisciplineNameCommand request, CancellationToken cancellationToken)
    {
        await disciplineNameRepository.DeleteAsync(request.Id, cancellationToken);
        return Unit.Value;
    }
}