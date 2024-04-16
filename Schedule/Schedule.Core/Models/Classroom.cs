namespace Schedule.Core.Models;

public class Classroom
{
    public int ClassroomId { get; set; }

    public string Cabinet { get; set; } = null!;

    public bool IsDeleted { get; set; }
}