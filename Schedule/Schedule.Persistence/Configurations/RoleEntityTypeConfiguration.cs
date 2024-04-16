using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(e => e.RoleId)
            .HasName("role_pk");

        builder.ToTable("role");

        builder.HasIndex(e => e.Name, "role_name_index")
            .IsUnique();

        builder.Property(e => e.RoleId)
            .UseIdentityAlwaysColumn()
            .HasColumnName("role_id");
        builder.Property(e => e.Name)
            .HasMaxLength(30)
            .HasColumnName("name");
    }
}