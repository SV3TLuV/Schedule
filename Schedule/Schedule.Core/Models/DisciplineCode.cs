namespace Schedule.Core.Models;

public class DisciplineCode
{
    public int DisciplineCodeId { get; set; }

    public string Code { get; set; } = null!;

    public virtual ICollection<Discipline> Disciplines { get; set; } = new List<Discipline>();
}