namespace Schedule.Core.Models;

public class Time
{
    public int TimeId { get; set; }

    public TimeSpan Start { get; set; }

    public TimeSpan End { get; set; }

    public int LessonNumber { get; set; }

    public int TypeId { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Lesson> Lessons { get; } = new List<Lesson>();

    public virtual TimeType Type { get; set; } = null!;
}