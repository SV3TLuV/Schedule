namespace Schedule.Persistence.Entities;

public partial class Template
{
    public int TemplateId { get; set; }

    public int GroupId { get; set; }

    public int TermId { get; set; }

    public int DayId { get; set; }

    public int WeekTypeId { get; set; }

    public virtual Day Day { get; set; } = null!;

    public virtual Group Group { get; set; } = null!;

    public virtual ICollection<Lesson> Lessons { get; } = new List<Lesson>();

    public virtual Term Term { get; set; } = null!;

    public virtual WeekType WeekType { get; set; } = null!;
}
