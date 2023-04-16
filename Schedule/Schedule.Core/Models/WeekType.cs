namespace Schedule.Core.Models;

public class WeekType
{
    public int WeekTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Date> Dates { get; set; } = new List<Date>();

    public virtual ICollection<Template> Templates { get; set; } = new List<Template>();
}