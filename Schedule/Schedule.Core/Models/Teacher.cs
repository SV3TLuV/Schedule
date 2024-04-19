namespace Schedule.Core.Models;

public class Teacher
{
    public int TeacherId { get; set; }

    public int AccountId { get; set; }

    public virtual Account Account { get; set; } = null!;
    
    public virtual ICollection<LessonTeacherClassroom> LessonTeacherClassrooms { get; set; } =
        new List<LessonTeacherClassroom>();
    
    public virtual ICollection<LessonChangeTeacherClassroom> LessonChangeTeacherClassrooms { get; set; } =
        new List<LessonChangeTeacherClassroom>();
}