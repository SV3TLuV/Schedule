namespace Schedule.Persistence.Entities;

public partial class ClassroomType
{
    public int ClassroomTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Classroom> Classrooms { get; } = new List<Classroom>();
}
