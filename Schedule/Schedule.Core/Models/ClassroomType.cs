namespace Schedule.Core.Models;

public class ClassroomType
{
    public int ClassroomTypeId { get; set; }

    public string Name { get; set; } = null!;

    public bool IsDeleted { get; set; }
    
    public virtual ICollection<ClassroomClassroomType> ClassroomClassroomTypes { get; set; } =
        new List<ClassroomClassroomType>();
}