using MediatR;
using Schedule.Application.Features.Base.Queries.Paginated;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Surnames.Queries.GetList;

public sealed record GetSurnameListQuery : PaginatedQuery, IRequest<PagedList<SurnameViewModel>>
{
    public string? Search { get; set; }
}