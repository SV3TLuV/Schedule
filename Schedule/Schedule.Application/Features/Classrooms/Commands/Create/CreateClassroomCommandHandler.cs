using MediatR;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Classrooms.Commands.Create;

public sealed class CreateClassroomCommandHandler(IClassroomRepository classroomRepository)
    : IRequestHandler<CreateClassroomCommand, int>
{
    public async Task<int> Handle(CreateClassroomCommand request, CancellationToken cancellationToken)
    {
        return await classroomRepository.AddIfNotExists(request.Cabinet, cancellationToken);
    }
}