namespace Schedule.Core.Models;

public class Teacher
{
    public int TeacherId { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string MiddleName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public virtual ICollection<LessonTeacherClassroom> LessonTeacherClassrooms { get; set; } =
        new List<LessonTeacherClassroom>();

    public virtual ICollection<LessonTemplateTeacherClassroom> LessonTemplateTeacherClassrooms { get; set; } =
        new List<LessonTemplateTeacherClassroom>();

    public virtual ICollection<Discipline> Disciplines { get; set; } = new List<Discipline>();

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
}