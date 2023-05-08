namespace Schedule.Core.Models;

public class TransferingGroupsHistory
{
    public int GroupId { get; set; }

    public int Year { get; set; }

    public bool IsTransferred { get; set; }

    public DateTime TransferDate { get; set; }

    public virtual Group Group { get; set; } = null!;
}