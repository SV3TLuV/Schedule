namespace Schedule.Core.Models;

public partial class LessonTeacherClassroom
{
    public int LessonId { get; set; }

    public int TeacherId { get; set; }

    public int? ClassroomId { get; set; }

    public virtual Classroom? Classroom { get; set; }

    public virtual Lesson Lesson { get; set; } = null!;

    public virtual Teacher Teacher { get; set; } = null!;
}

public partial class LessonTeacherClassroom : IEquatable<LessonTemplateTeacherClassroom>
{
    public bool Equals(LessonTemplateTeacherClassroom? other)
    {
        return other is not null &&
               ClassroomId == other.ClassroomId &&
               TeacherId == other.TeacherId;
    }
}