using MediatR;

namespace Schedule.Application.Features.LessonTemplates.Commands.Delete;

public sealed record DeleteLessonTemplateCommand(int Id) : IRequest<Unit>;