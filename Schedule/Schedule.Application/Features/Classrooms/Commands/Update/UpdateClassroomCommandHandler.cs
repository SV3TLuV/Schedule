using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Classrooms.Commands.Update;

public sealed class UpdateClassroomCommandHandler(
    IMapper mapper,
    IClassroomRepository classroomRepository)
    : IRequestHandler<UpdateClassroomCommand, Unit>
{
    public async Task<Unit> Handle(UpdateClassroomCommand request, CancellationToken cancellationToken)
    {
        var classroom = mapper.Map<Classroom>(request);
        await classroomRepository.UpdateAsync(classroom, cancellationToken);
        return Unit.Value;
    }
}