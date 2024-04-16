namespace Schedule.Core.Models;

public class EmployeePermission
{
    public int EmployeeId { get; set; }

    public int PermissionId { get; set; }

    public bool HasAccess { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Permission Permission { get; set; } = null!;
}