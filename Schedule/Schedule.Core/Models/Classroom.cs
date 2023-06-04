namespace Schedule.Core.Models;

public class Classroom
{
    public int ClassroomId { get; set; }

    public string Cabinet { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public virtual ICollection<LessonTeacherClassroom> LessonTeacherClassrooms { get; set; } =
        new List<LessonTeacherClassroom>();

    public virtual ICollection<LessonTemplateTeacherClassroom> LessonTemplateTeacherClassrooms { get; set; } =
        new List<LessonTemplateTeacherClassroom>();

    public virtual ICollection<ClassroomClassroomType> ClassroomClassroomTypes { get; set; } =
        new List<ClassroomClassroomType>();
}