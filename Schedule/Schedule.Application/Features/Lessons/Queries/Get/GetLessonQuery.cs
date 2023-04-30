using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Lessons.Queries.Get;

public sealed record GetLessonQuery(int Id) : IRequest<LessonViewModel>;