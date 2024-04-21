using MediatR;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Timetables.Queries.GetCurrentTimetableList;

public sealed class GetCurrentTimetableListQuery : IRequest<PagedList<TimetableViewModel>>
{
    
}