namespace Schedule.Core.Models;

public class Discipline
{
    public int DisciplineId { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public int TotalHours { get; set; }

    public int TermId { get; set; }

    public int SpecialityCodeId { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<LessonTemplate> LessonTemplates { get; set; } = new List<LessonTemplate>();

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual SpecialityCode SpecialityCode { get; set; } = null!;

    public virtual Term Term { get; set; } = null!;

    public virtual ICollection<TeacherDiscipline> TeacherDisciplines { get; set; } =
        new List<TeacherDiscipline>();
}