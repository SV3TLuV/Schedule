namespace Schedule.Core.Models;

public class Discipline
{
    public int DisciplineId { get; set; }

    public int DisciplineNameId { get; set; }

    public int DisciplineCodeId { get; set; }

    public int TotalHours { get; set; }

    public int TermId { get; set; }

    public int SpecialityId { get; set; }

    public int DisciplineTypeId { get; set; }

    public bool IsDeleted { get; set; }

    public virtual DisciplineCode DisciplineCode { get; set; } = null!;

    public virtual DisciplineName DisciplineName { get; set; } = null!;

    public virtual DisciplineType DisciplineType { get; set; } = null!;

    public virtual ICollection<LessonChange> LessonChanges { get; set; } = new List<LessonChange>();

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual Speciality Speciality { get; set; } = null!;

    public virtual Term Term { get; set; } = null!;
}