namespace Schedule.Core.Models;

public class Term
{
    public int TermId { get; set; }

    public int CourseId { get; set; }

    public int CourseTerm { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<Discipline> Disciplines { get; } = new List<Discipline>();

    public virtual ICollection<Template> Templates { get; } = new List<Template>();
}