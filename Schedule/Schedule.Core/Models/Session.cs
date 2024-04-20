namespace Schedule.Core.Models;

public class Session
{
    public Guid SessionId { get; set; }

    public string RefreshToken { get; set; } = null!;

    public DateTime Created { get; set; }

    public DateTime? Updated { get; set; }

    public int AccountId { get; set; }

    public virtual Account Account { get; set; } = null!;
}