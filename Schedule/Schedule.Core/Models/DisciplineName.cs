namespace Schedule.Core.Models;

public class DisciplineName
{
    public int DisciplineNameId { get; set; }

    public string Name { get; set; } = null!;

    public bool IsDeleted { get; set; }
    
    public ICollection<Discipline> Disciplines { get; set; } = null!;
}