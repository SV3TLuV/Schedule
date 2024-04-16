namespace Schedule.Core.Models;

public class MiddleName
{
    public string MiddleName1 { get; set; } = null!;

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}