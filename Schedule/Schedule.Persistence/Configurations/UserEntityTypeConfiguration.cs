using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(e => e.Login, "IX_Users").IsUnique();
        builder.Property(e => e.Login).HasMaxLength(50);
        builder.Property(e => e.PasswordHash).HasMaxLength(512);
        builder.HasOne(d => d.Role).WithMany(p => p.Users)
            .HasForeignKey(d => d.RoleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Users_Roles");
        builder.HasData(new User[]
        {
            new()
            {
                UserId = 1,
                Login = "Admin",
                PasswordHash = "$2a$11$/AKGJmbjT9.J/pdMmIk7S.VItgYYrknXhoPAUsTRIUqzIUXVw25zq",
                RoleId = 1,
            },
            new()
            {
                UserId = 2,
                Login = "Editor",
                PasswordHash = "$2a$11$qtS1HuNq4Q/9/gnERQJunu9U0wEYvtxbN2Z8senRvOLUF1gn/OV3i",
                RoleId = 2,
            },
        });
    }
}