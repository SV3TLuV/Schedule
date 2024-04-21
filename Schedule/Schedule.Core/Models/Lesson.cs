namespace Schedule.Core.Models;

public class Lesson
{
    public int LessonId { get; set; }

    public int? DisciplineId { get; set; }

    public int Number { get; set; }

    public int? Subgroup { get; set; }

    public int TimetableId { get; set; }
    
    public TimeOnly TimeStart { get; set; }
    
    public TimeOnly TimeEnd { get; set; }

    public virtual Discipline? Discipline { get; set; }

    public virtual ICollection<LessonChange>? LessonChanges { get; set; }

    public virtual Timetable Timetable { get; set; } = null!;

    public virtual ICollection<LessonTeacherClassroom> LessonTeacherClassrooms { get; set; } =
        new List<LessonTeacherClassroom>();
}