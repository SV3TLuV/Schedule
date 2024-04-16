namespace Schedule.Core.Models;

public class Lesson
{
    public int LessonId { get; set; }

    public int DisciplineId { get; set; }

    public int Number { get; set; }

    public int? Subgroup { get; set; }

    public int TimetableId { get; set; }

    public List<int> TeacherIds { get; set; } = null!;

    public List<int> ClassroomIds { get; set; } = null!;

    public int? LessonChangeId { get; set; }

    public int? TimeId { get; set; }

    public virtual Discipline Discipline { get; set; } = null!;

    public virtual LessonChange? LessonChange { get; set; }

    public virtual ICollection<LessonChange> LessonChanges { get; set; } = new List<LessonChange>();

    public virtual Time? Time { get; set; }

    public virtual Timetable Timetable { get; set; } = null!;
}