using MediatR;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Timetables.Queries.GetCurrentList;

public sealed record GetCurrentTimetableListQuery(int? GroupId, int DateCount = 2) : IRequest<PagedList<CurrentTimetableViewModel>>;