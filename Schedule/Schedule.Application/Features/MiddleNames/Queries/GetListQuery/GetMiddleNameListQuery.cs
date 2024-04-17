using MediatR;
using Schedule.Application.Features.Base.Queries.Paginated;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Application.Features.MiddleNames.Queries.GetListQuery;

public sealed record GetMiddleNameListQuery : PaginatedQuery, IRequest<PagedList<MiddleNameViewModel>>
{
    public string? Search { get; set; }
}