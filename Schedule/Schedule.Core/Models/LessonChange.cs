namespace Schedule.Core.Models;

public class LessonChange
{
    public int LessonChangeId { get; set; }

    public int Number { get; set; }

    public int? Subgroup { get; set; }

    public TimeOnly TimeStart { get; set; }
    
    public TimeOnly TimeEnd { get; set; }

    public int LessonId { get; set; }

    public int DisciplineId { get; set; }

    public DateOnly Date { get; set; }

    public virtual Discipline Discipline { get; set; } = null!;

    public virtual Lesson Lesson { get; set; } = null!;

    public virtual ICollection<LessonChangeTeacherClassroom> LessonChangeTeacherClassrooms { get; set; } =
        new List<LessonChangeTeacherClassroom>();
}