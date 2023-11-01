namespace Schedule.Core.Models;

public class Discipline
{
    public int DisciplineId { get; set; }
    
    public int NameId { get; set; }

    public int CodeId { get; set; }
    
    public int TotalHours { get; set; }

    public int TermId { get; set; }

    public int SpecialityId { get; set; }

    public int DisciplineTypeId { get; set; }

    public bool IsDeleted { get; set; }
    
    public DisciplineName Name { get; set; } = null!;
    
    public DisciplineCode Code { get; set; } = null!;
    
    public virtual DisciplineType DisciplineType { get; set; } = null!;

    public virtual ICollection<LessonTemplate> LessonTemplates { get; set; } = new List<LessonTemplate>();

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual Speciality Speciality { get; set; } = null!;

    public virtual Term Term { get; set; } = null!;
}