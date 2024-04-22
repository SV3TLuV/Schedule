namespace Schedule.Core.Models;

public class Discipline
{
    public int DisciplineId { get; set; }

    public int NameId { get; set; }

    public int CodeId { get; set; }

    public int TotalHours { get; set; }

    public int TermId { get; set; }

    public int SpecialityId { get; set; }

    public int TypeId { get; set; }

    public bool IsDeleted { get; set; }

    public virtual DisciplineCode Code { get; set; } = null!;

    public virtual DisciplineName Name { get; set; } = null!;

    public virtual DisciplineType Type { get; set; } = null!;

    public virtual ICollection<LessonChange> LessonChanges { get; set; } = new List<LessonChange>();

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual Speciality Speciality { get; set; } = null!;

    public virtual Term Term { get; set; } = null!;
}