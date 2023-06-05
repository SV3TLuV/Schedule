using MediatR;

namespace Schedule.Application.Features.ClassroomTypes.Commands.Delete;

public sealed record DeleteClassroomTypeCommand(int Id) : IRequest<Unit>;