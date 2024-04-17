using MediatR;
using Schedule.Application.Features.Base.Queries.Paginated;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Names.Queries;

public sealed record GetNameListQuery : PaginatedQuery, IRequest<PagedList<NameViewModel>>
{
    public string? Search { get; set; }
}