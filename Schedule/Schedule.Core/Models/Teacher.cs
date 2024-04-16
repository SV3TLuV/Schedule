namespace Schedule.Core.Models;

public class Teacher
{
    public int TeacherId { get; set; }

    public int AccountId { get; set; }

    public virtual Account Account { get; set; } = null!;
}