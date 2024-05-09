using System.Collections;

namespace Schedule.Application.ViewModels;

public class CurrentTimetableViewModel
{
    public required GroupViewModel Group { get; set; }
    public required ICollection<GroupedViewModel<RecordValue<DayViewModel, DateOnly>, TimetableViewModel>> DaysAndDate { get; set; }
}

public class RecordValue<T, K>
{
    public T TValue { get; set; }
    public K KValue { get; set; }
}