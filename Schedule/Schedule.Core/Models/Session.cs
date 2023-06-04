namespace Schedule.Core.Models;

public class Session
{
    public Guid SessionId { get; set; }

    public string RefreshToken { get; set; } = null!;

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}