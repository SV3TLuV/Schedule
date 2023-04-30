namespace Schedule.Core.Models;

public partial class Lesson
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

public partial class Lesson : IEquatable<LessonTemplate>
{
    public bool Equals(LessonTemplate? other)
    {
        if (other is null)
            return false;
        if (Number != other.Number)
            return false;
        if (Subgroup != other.Subgroup)
            return false;
        if (TimeId != other.TimeId)
            return false;
        if (DisciplineId != other.DisciplineId)
            return false;
        if (LessonTeacherClassrooms.Count != other.LessonTemplateTeacherClassrooms.Count)
            return false;

        for (var i = 0; i < LessonTeacherClassrooms.Count; i++)
        {
            var lessonTeacherClassroom = LessonTeacherClassrooms.ElementAt(i);
            var templateTeacherClassroom = other.LessonTemplateTeacherClassrooms.ElementAt(i);
            
            if (!lessonTeacherClassroom.Equals(templateTeacherClassroom))
                return false;
        }
        
        return true;
    }
}