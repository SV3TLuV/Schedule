namespace Schedule.Core.Models;

public class Employee
{
    public int EmployeeId { get; set; }

    public int AccountId { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<EmployeePermission> EmployeePermissions { get; set; } = new List<EmployeePermission>();
}