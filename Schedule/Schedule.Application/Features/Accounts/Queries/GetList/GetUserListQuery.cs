using MediatR;
using Schedule.Application.Features.Base.Queries.Paginated;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Users.Queries.GetList;

public sealed record GetUserListQuery : PaginatedQuery, IRequest<PagedList<UserViewModel>>;