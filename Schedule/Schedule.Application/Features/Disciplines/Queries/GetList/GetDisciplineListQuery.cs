using MediatR;
using Schedule.Application.Common.Enums;
using Schedule.Application.Features.Base.Queries.Paginated;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Enums;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Disciplines.Queries.GetList;

public sealed record GetDisciplineListQuery : PaginatedQuery, IRequest<PagedList<DisciplineViewModel>>
{
    public required QueryFilter Filter { get; init; } = QueryFilter.Available;
    public string? Search { get; set; }
}