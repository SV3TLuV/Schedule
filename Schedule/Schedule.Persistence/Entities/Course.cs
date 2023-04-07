namespace Schedule.Persistence.Entities;

public partial class Course
{
    public int CourseId { get; set; }

    public virtual ICollection<Group> Groups { get; } = new List<Group>();

    public virtual ICollection<Term> Terms { get; } = new List<Term>();
}
