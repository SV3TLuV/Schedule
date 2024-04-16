using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class AccountEntityTypeConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(e => e.AccountId)
            .HasName("account_pk");

        builder.ToTable("account");

        builder.HasIndex(e => e.Email, "account_email_index")
            .IsUnique();

        builder.HasIndex(e => e.Login, "account_login_index")
            .IsUnique();

        builder.Property(e => e.AccountId)
            .UseIdentityAlwaysColumn()
            .HasColumnName("account_id");
        builder.Property(e => e.Email)
            .HasMaxLength(200)
            .HasColumnName("email");
        builder.Property(e => e.Login)
            .HasMaxLength(50)
            .HasColumnName("login");
        builder.Property(e => e.MiddleName)
            .HasMaxLength(40)
            .HasColumnName("middle_name");
        builder.Property(e => e.Name)
            .HasMaxLength(40)
            .HasColumnName("name");
        builder.Property(e => e.PasswordHash)
            .HasMaxLength(512)
            .HasColumnName("password_hash");
        builder.Property(e => e.RoleId)
            .HasColumnName("role_id");
        builder.Property(e => e.Surname)
            .HasMaxLength(40)
            .HasColumnName("surname");
        builder.Property(e => e.IsDeleted)
            .HasColumnName("is_deleted");

        builder.HasOne(d => d.MiddleNameNavigation)
            .WithMany(p => p.Accounts)
            .HasForeignKey(d => d.MiddleName)
            .HasConstraintName("account_middle_name_fk");

        builder.HasOne(d => d.NameNavigation)
            .WithMany(p => p.Accounts)
            .HasForeignKey(d => d.Name)
            .HasConstraintName("account_name_fk");

        builder.HasOne(d => d.Role)
            .WithMany(p => p.Accounts)
            .HasForeignKey(d => d.RoleId)
            .HasConstraintName("account_role_id_fk");

        builder.HasOne(d => d.SurnameNavigation)
            .WithMany(p => p.Accounts)
            .HasForeignKey(d => d.Surname)
            .HasConstraintName("account_surname_fk");
    }
}