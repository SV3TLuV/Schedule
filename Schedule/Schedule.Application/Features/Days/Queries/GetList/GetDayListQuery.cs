using MediatR;
using Schedule.Application.Features.Base.Queries.Paginated;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Days.Queries.GetList;

public sealed record GetDayListQuery : PaginatedQuery, IRequest<PagedList<DayViewModel>>;