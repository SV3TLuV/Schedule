using MediatR;
using Schedule.Application.Features.Base.Queries.Paginated;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Enums;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Teachers.Queries.GetList;

public sealed record GetTeacherListQuery : PaginatedQuery, IRequest<PagedList<TeacherViewModel>>
{
    public required QueryFilter Filter { get; set; } = QueryFilter.Available;
}