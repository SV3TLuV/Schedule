namespace Schedule.Core.Models;

public class DisciplineCode
{
    public int DisciplineCodeId { get; set; }

    public string Code { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public virtual ICollection<Discipline> Disciplines { get; set; } = new List<Discipline>();
}