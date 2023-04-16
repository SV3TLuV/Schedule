namespace Schedule.Core.Models;

public class Lesson
{
    public int LessonId { get; set; }

    public int Number { get; set; }

    public int? Subgroup { get; set; }

    public int? TimeId { get; set; }

    public int TimetableId { get; set; }

    public int? DisciplineId { get; set; }

    public bool IsChanged { get; set; }

    public virtual Discipline? Discipline { get; set; }

    public virtual ICollection<LessonTeacherClassroom> LessonTeacherClassrooms { get; set; } =
        new List<LessonTeacherClassroom>();

    public virtual Time? Time { get; set; }

    public virtual Timetable Timetable { get; set; } = null!;
}