namespace Schedule.Core.Models;

public class ClassroomType
{
    public int ClassroomTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Classroom> Classrooms { get; } = new List<Classroom>();
}