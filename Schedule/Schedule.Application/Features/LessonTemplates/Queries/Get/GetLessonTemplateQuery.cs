using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.LessonTemplates.Queries.Get;

public sealed record GetLessonTemplateQuery(int Id) : IRequest<LessonTemplateViewModel>;