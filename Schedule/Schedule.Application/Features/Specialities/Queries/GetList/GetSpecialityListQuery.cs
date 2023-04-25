using MediatR;
using Schedule.Application.Features.Base.Queries.Paginated;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Enums;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Specialities.Queries.GetList;

public sealed record GetSpecialityListQuery : PaginatedQuery, IRequest<PagedList<SpecialityViewModel>>
{
    public required QueryFilter Filter { get; init; } = QueryFilter.Available;
}