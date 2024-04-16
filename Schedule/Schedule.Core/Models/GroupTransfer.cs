namespace Schedule.Core.Models;

public class GroupTransfer
{
    public int NextTermId { get; set; }

    public int GroupId { get; set; }

    public bool IsTransferred { get; set; }

    public DateOnly TransferDate { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual Term NextTerm { get; set; } = null!;
}