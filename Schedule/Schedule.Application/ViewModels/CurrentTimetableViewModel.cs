namespace Schedule.Application.ViewModels;

public class CurrentTimetableViewModel
{
    public required GroupViewModel Group { get; set; }
    public required ICollection<GroupedViewModel<DateViewModel, TimetableViewModel>> Dates { get; set; }
}