using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.EmployeeId)
            .HasName("employee_pk");

        builder.ToTable("employee");

        builder.Property(e => e.EmployeeId)
            .UseIdentityAlwaysColumn()
            .HasColumnName("employee_id");
        builder.Property(e => e.AccountId)
            .HasColumnName("account_id");

        builder.HasOne(d => d.Account)
            .WithMany(p => p.Employees)
            .HasForeignKey(d => d.AccountId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("employee_account_id_fk");
    }
}