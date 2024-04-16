namespace Schedule.Core.Models;

public class Surname
{
    public string Surname1 { get; set; } = null!;

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}