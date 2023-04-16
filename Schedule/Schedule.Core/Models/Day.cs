namespace Schedule.Core.Models;

public class Day
{
    public int DayId { get; set; }

    public string Name { get; set; } = null!;

    public bool IsStudy { get; set; }

    public virtual ICollection<Date> Dates { get; set; } = new List<Date>();

    public virtual ICollection<Template> Templates { get; set; } = new List<Template>();
}