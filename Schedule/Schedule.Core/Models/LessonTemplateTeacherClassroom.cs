namespace Schedule.Core.Models;

public class LessonTemplateTeacherClassroom
{
    public int LessonTemplateId { get; set; }

    public int TeacherId { get; set; }

    public int? ClassroomId { get; set; }

    public virtual Classroom? Classroom { get; set; }

    public virtual LessonTemplate LessonTemplate { get; set; } = null!;

    public virtual Teacher Teacher { get; set; } = null!;
}