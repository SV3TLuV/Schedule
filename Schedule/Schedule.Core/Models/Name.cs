namespace Schedule.Core.Models;

public class Name
{
    public string Name1 { get; set; } = null!;

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}