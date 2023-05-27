namespace Schedule.Core.Models;

public class Term
{
    public int TermId { get; set; }

    public int CourseId { get; set; }

    public int CourseTerm { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<Discipline> Disciplines { get; set; } = new List<Discipline>();

    public virtual ICollection<GroupTransfer> GroupTransfers { get; set; } = new List<GroupTransfer>();

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual ICollection<Speciality> Specialities { get; set; } = new List<Speciality>();

    public virtual ICollection<Template> Templates { get; set; } = new List<Template>();
}