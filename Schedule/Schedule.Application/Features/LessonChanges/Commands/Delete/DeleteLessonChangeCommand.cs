using MediatR;

namespace Schedule.Application.Features.LessonChanges.Commands.Delete;

public sealed record DeleteLessonChangeCommand(int Id) : IRequest<Unit>;