using MediatR;
using Schedule.Application.Features.Base.Queries.Paginated;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Enums;
using Schedule.Core.Models;

namespace Schedule.Application.Features.ClassroomTypes.Queries.GetList;

public sealed record GetClassroomTypeListQuery
    : PaginatedQuery, IRequest<PagedList<ClassroomTypeViewModel>>
{
    public required QueryFilter Filter { get; init; } = QueryFilter.Available;
    public string? Search { get; set; } 
}