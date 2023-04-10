using MediatR;
using Schedule.Application.Features.Base.Queries.Paginated;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Dates.Queries.GetList;

public sealed record GetDateListQuery : PaginatedQuery, IRequest<PagedList<DateViewModel>>;