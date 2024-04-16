namespace Schedule.Core.Models;

public class Permission
{
    public int PermissionId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<EmployeePermission> EmployeePermissions { get; set; } = new List<EmployeePermission>();
}