using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Classrooms.Commands.Delete;

public sealed class DeleteClassroomCommandHandler(IClassroomRepository classroomRepository)
    : IRequestHandler<DeleteClassroomCommand, Unit>
{
    public async Task<Unit> Handle(DeleteClassroomCommand request, CancellationToken cancellationToken)
    {
        await classroomRepository.DeleteAsync(request.Id, cancellationToken);
        return Unit.Value;
    }
}