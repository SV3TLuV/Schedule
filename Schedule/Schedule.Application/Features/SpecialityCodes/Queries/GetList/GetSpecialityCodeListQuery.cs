using MediatR;
using Schedule.Application.Features.Base.Queries.Paginated;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Enums;
using Schedule.Core.Models;

namespace Schedule.Application.Features.SpecialityCodes.Queries.GetList;

public sealed record GetSpecialityCodeListQuery : PaginatedQuery, IRequest<PagedList<SpecialityCodeViewModel>>
{
    public required QueryFilter Filter { get; init; } = QueryFilter.Available;
}