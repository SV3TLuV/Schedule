using MediatR;
using Schedule.Application.Features.Base.Queries.Paginated;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Timetables.Queries.GetList;

public sealed record GetTimetableListQuery :
    PaginatedQuery, IRequest<PagedList<TimetableViewModel>>;