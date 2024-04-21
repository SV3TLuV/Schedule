using MediatR;
using Schedule.Application.Features.Base.Queries.Paginated;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Timetables.Queries.GetCurrentTimetableList;

public sealed record GetCurrentTimetableListQuery : PaginatedQuery, IRequest<PagedList<CurrentTimetableViewModel>>
{
    public int? GroupId { get; init; }
    public required int DayCount { get; init; } = 2;
}