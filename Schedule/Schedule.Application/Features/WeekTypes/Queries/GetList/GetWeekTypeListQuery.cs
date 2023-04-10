using MediatR;
using Schedule.Application.Features.Base.Queries.Paginated;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Application.Features.WeekTypes.Queries.GetList;

public sealed record GetWeekTypeListQuery : PaginatedQuery, IRequest<PagedList<WeekTypeViewModel>>;