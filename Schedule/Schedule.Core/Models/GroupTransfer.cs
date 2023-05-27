namespace Schedule.Core.Models;

public class GroupTransfer
{
    public int GroupId { get; set; }

    public int NextTermId { get; set; }

    public bool IsTransferred { get; set; }

    public DateTime TransferDate { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual Term NextTerm { get; set; } = null!;
}