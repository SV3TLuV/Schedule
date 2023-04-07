namespace Schedule.Persistence.Entities;

public class Course
{
    public int CourseId { get; set; }

    public virtual ICollection<Group> Groups { get; } = new List<Group>();

    public virtual ICollection<Term> Terms { get; } = new List<Term>();
}