namespace Schedule.Application.ViewModels;

public class CurrentTimetableViewModel
{
    public required GroupViewModel Group { get; set; }
    public required ICollection<GroupedViewModel<DayViewModel, TimetableViewModel>> Days { get; set; }
}