using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Disciplines.Commands.Delete;

public sealed class DeleteDisciplineCommandHandler(IDisciplineRepository disciplineRepository)
    : IRequestHandler<DeleteDisciplineCommand, Unit>
{
    public async Task<Unit> Handle(DeleteDisciplineCommand request, CancellationToken cancellationToken)
    {
        await disciplineRepository.DeleteAsync(request.Id, cancellationToken);
        return Unit.Value;
    }
}