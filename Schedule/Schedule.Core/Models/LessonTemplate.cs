namespace Schedule.Core.Models;

public class LessonTemplate
{
    public int LessonTemplateId { get; set; }

    public int Number { get; set; }

    public int? Subgroup { get; set; }

    public int? TimeId { get; set; }

    public int TemplateId { get; set; }

    public int? DisciplineId { get; set; }

    public virtual Discipline? Discipline { get; set; }

    public virtual ICollection<LessonTemplateTeacherClassroom> LessonTemplateTeacherClassrooms { get; set; } =
        new List<LessonTemplateTeacherClassroom>();

    public virtual Template Template { get; set; } = null!;

    public virtual Time? Time { get; set; }
}