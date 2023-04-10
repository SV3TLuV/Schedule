using MediatR;
using Schedule.Application.Features.Base.Queries.Paginated;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Queries.GetList;

public sealed record GetGroupListQuery : PaginatedQuery, IRequest<PagedList<GroupViewModel>>;