using MediatR;
using Schedule.Application.Features.Base.Queries.Paginated;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Roles.Queries.GetList;

public sealed record GetRoleListQuery : PaginatedQuery, IRequest<PagedList<RoleViewModel>>;