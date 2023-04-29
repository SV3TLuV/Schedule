namespace Schedule.Application.ViewModels;

public class CurrentTimetableViewModel
{
    public required ICollection<GroupViewModel> Groups { get; set; }
    public required ICollection<GroupedViewModel<DateViewModel, TimetableViewModel>> Dates { get; set; }
}