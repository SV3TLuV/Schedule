namespace Schedule.Core.Models;

public class WeekType
{
    public int WeekTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Timetable> Timetables { get; set; } = new List<Timetable>();
}