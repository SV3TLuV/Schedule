namespace Schedule.Core.Models;

public class LessonTeacherClassroom
{
    public int LessonId { get; set; }
    
    public int TeacherId { get; set; }
    
    public int ClassroomId { get; set; }

    public virtual Lesson Lesson { get; set; } = null!;

    public virtual Teacher Teacher { get; set; } = null!;

    public virtual Classroom Classroom { get; set; } = null!;
}