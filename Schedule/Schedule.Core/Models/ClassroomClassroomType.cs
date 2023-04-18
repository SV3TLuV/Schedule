namespace Schedule.Core.Models;

public class ClassroomClassroomType
{
    public int ClassroomId { get; set; }
    
    public Classroom Classroom { get; set; } = null!;

    public int ClassroomTypeId { get; set; }

    public ClassroomType ClassroomType { get; set; } = null!;
}