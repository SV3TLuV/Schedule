using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class EmployeePermissionEntityTypeConfiguration : IEntityTypeConfiguration<EmployeePermission>
{
    public void Configure(EntityTypeBuilder<EmployeePermission> builder)
    {
        builder.HasKey(e => new { e.EmployeeId, e.PermissionId })
            .HasName("employee_permission_pk");

        builder.ToTable("employee_permission");

        builder.Property(e => e.EmployeeId)
            .HasColumnName("employee_id");
        builder.Property(e => e.PermissionId)
            .HasColumnName("permission_id");
        builder.Property(e => e.HasAccess)
            .HasDefaultValue(false)
            .HasColumnName("has_access");

        builder.HasOne(d => d.Employee)
            .WithMany(p => p.EmployeePermissions)
            .HasForeignKey(d => d.EmployeeId)
            .HasConstraintName("employee_permission_employee_id_fk");

        builder.HasOne(d => d.Permission)
            .WithMany(p => p.EmployeePermissions)
            .HasForeignKey(d => d.PermissionId)
            .HasConstraintName("employee_permission_permission_id_fk");
    }
}