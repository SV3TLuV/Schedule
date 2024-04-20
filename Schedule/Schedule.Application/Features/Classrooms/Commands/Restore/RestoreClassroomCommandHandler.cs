using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Classrooms.Commands.Restore;

public sealed class RestoreClassroomCommandHandler(IClassroomRepository classroomRepository)
    : IRequestHandler<RestoreClassroomCommand, Unit>
{
    public async Task<Unit> Handle(RestoreClassroomCommand request,
        CancellationToken cancellationToken)
    {
        await classroomRepository.RestoreAsync(request.Id, cancellationToken);
        return Unit.Value;
    }
}