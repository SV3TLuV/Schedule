using MediatR;

namespace Schedule.Application.Features.Lessons.Queries.GetLessonNumberList;

public sealed record GetLessonNumberListQuery(int DateId) : IRequest<ICollection<int>>;