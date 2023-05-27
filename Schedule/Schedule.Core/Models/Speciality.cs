namespace Schedule.Core.Models;

public class Speciality
{
    public int SpecialityId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int MaxTermId { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Discipline> Disciplines { get; set; } = new List<Discipline>();

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual Term MaxTerm { get; set; } = null!;
}