namespace Schedule.Core.Models;

public class Session
{
    public int SessionId { get; set; }

    public string RefreshToken { get; set; } = null!;

    public DateOnly Created { get; set; }

    public DateOnly? Updated { get; set; }

    public int AccountId { get; set; }

    public virtual Account Account { get; set; } = null!;
}