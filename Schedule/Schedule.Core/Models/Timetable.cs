namespace Schedule.Core.Models;

public class Timetable
{
    public int TimetableId { get; set; }

    public int GroupId { get; set; }

    public DateOnly Created { get; set; }

    public DateOnly? Ended { get; set; }

    public int DayId { get; set; }

    public int WeekTypeId { get; set; }

    public virtual Day Day { get; set; } = null!;

    public virtual Group Group { get; set; } = null!;

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual WeekType WeekType { get; set; } = null!;
}