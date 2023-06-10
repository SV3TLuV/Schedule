using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(e => e.RoleId);
        builder.HasIndex(e => e.Name, "IX_Roles").IsUnique();
        builder.Property(e => e.Name).HasMaxLength(30);
        builder.HasData(new Role[]
        {
            new()
            {
                RoleId = 1,
                Name = "Admin",
            },
            new()
            {
                RoleId = 2,
                Name = "Editor",
            }
        });
    }
}