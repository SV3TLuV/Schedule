using MediatR;

namespace Schedule.Application.Features.Lessons.Commands.Delete;

public sealed record DeleteLessonCommand(int Id) : IRequest;