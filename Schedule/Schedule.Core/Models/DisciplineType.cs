namespace Schedule.Core.Models;

public class DisciplineType
{
    public int DisciplineTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Discipline> Disciplines { get; set; } = new List<Discipline>();
}