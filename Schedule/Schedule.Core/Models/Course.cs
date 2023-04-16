namespace Schedule.Core.Models;

public class Course
{
    public int CourseId { get; set; }

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual ICollection<Term> Terms { get; set; } = new List<Term>();
}