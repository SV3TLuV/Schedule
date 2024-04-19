namespace Schedule.Core.Models;

public class LessonChangeTeacherClassroom
{
    public int LessonChangeId { get; set; }
    
    public int TeacherId { get; set; }
    
    public int ClassroomId { get; set; }
    
    public virtual LessonChange LessonChange { get; set; } = null!;

    public virtual Teacher Teacher { get; set; } = null!;

    public virtual Classroom Classroom { get; set; } = null!;
}