namespace Schedule.Core.Models;

public class Day
{
    public int DayId { get; set; }

    public string Name { get; set; } = null!;

    public bool IsStudy { get; set; }

    public virtual ICollection<Date> Dates { get; } = new List<Date>();

    public virtual ICollection<Template> Templates { get; } = new List<Template>();
}