using MediatR;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Enums;

namespace Schedule.Application.Features.Timetables.Queries.GetList;

public class GetTimetableListQuery : IRequest<ICollection<TimetableViewModel>>
{
    public int? GroupId { get; set; }
    public int? DayId { get; set; }
    public WeekType? WeekTypeId { get; set; }
    public DateOnly? Date { get; set; }
}