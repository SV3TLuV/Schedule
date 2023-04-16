namespace Schedule.Core.Models;

public class SpecialityCode
{
    public int SpecialityCodeId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public virtual ICollection<Discipline> Disciplines { get; set; } = new List<Discipline>();

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
}