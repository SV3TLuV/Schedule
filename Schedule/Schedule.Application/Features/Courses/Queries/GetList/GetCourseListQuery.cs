using MediatR;
using Schedule.Application.Features.Base.Queries.Paginated;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Courses.Queries.GetList;

public sealed record GetCourseListQuery : PaginatedQuery, IRequest<PagedList<CourseViewModel>>;