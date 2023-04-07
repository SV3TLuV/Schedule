namespace Schedule.Persistence.Entities;

public class Discipline
{
    public int DisciplineId { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public int TotalHours { get; set; }

    public int TermId { get; set; }

    public int SpecialityCodeId { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Lesson> Lessons { get; } = new List<Lesson>();

    public virtual SpecialityCode SpecialityCode { get; set; } = null!;

    public virtual Term Term { get; set; } = null!;

    public virtual ICollection<Teacher> Teachers { get; } = new List<Teacher>();
}