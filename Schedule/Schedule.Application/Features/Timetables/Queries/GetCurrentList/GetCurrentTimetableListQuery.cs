using MediatR;
using Schedule.Application.Features.Base.Queries.Paginated;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Timetables.Queries.GetCurrentList;

public sealed record GetCurrentTimetableListQuery
    : PaginatedQuery, IRequest<PagedList<CurrentTimetableViewModel>>
{
    public int? GroupId { get; init; }
    public required int DateCount { get; init; } = 2;
}