using MediatR;
using Schedule.Application.Features.Base.Queries.Paginated;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Accounts.Queries.GetList;

public sealed record GetAccountListQuery : PaginatedQuery, IRequest<PagedList<AccountViewModel>>;