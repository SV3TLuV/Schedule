namespace Schedule.Core.Models;

public class Time
{
    public int TimeId { get; set; }

    public TimeSpan Start { get; set; }

    public TimeSpan End { get; set; }

    public int LessonNumber { get; set; }

    public int TypeId { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<LessonTemplate> LessonTemplates { get; set; } = new List<LessonTemplate>();

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual TimeType Type { get; set; } = null!;
}