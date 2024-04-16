using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class PermissionEntityTypeConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(e => e.PermissionId)
            .HasName("permission_pk");

        builder.ToTable("permission");

        builder.HasIndex(e => e.Name, "permission_name_index")
            .IsUnique();

        builder.Property(e => e.PermissionId)
            .UseIdentityAlwaysColumn()
            .HasColumnName("permission_id");
        builder.Property(e => e.Name)
            .HasMaxLength(40)
            .HasColumnName("name");
    }
}