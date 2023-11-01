using MediatR;
using Schedule.Application.Features.Base.Queries.Paginated;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Enums;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineCodes.Queries.GetList;

public sealed record GetDisciplineCodeListQuery : PaginatedQuery, IRequest<PagedList<DisciplineCodeViewModel>>
{
    public required QueryFilter Filter { get; init; } = QueryFilter.Available;
    public string? Search { get; set; }
}