namespace Schedule.Application.ViewModels;

public class CurrentTimetableViewModel
{
    public required ICollection<GroupViewModel> Groups { get; set; }
    public string GroupNames => string.Join(" ", Groups.Select(g => g.Name).Distinct());
    public required ICollection<GroupedViewModel<DateViewModel, TimetableViewModel>> Dates { get; set; }
}