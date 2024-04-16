namespace Schedule.Core.Models;

public class Time
{
    public int TimeId { get; set; }

    public TimeOnly Start { get; set; }

    public TimeOnly End { get; set; }

    public int Duration { get; set; }

    public int LessonNumber { get; set; }

    public int TypeId { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<LessonChange> LessonChanges { get; set; } = new List<LessonChange>();

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual TimeType Type { get; set; } = null!;
}