namespace Schedule.Core.Models;

public class Name
{
    public string Value { get; set; } = null!;

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}