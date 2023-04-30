using MediatR;
using Schedule.Application.Features.Base.Queries.Paginated;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Lessons.Queries.GetList;

public sealed record GetLessonListQuery : PaginatedQuery, IRequest<PagedList<LessonViewModel>>;