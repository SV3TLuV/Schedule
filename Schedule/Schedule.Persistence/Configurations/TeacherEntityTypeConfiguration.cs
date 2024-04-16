using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class TeacherEntityTypeConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.HasKey(e => e.TeacherId)
            .HasName("teacher_pk");

        builder.ToTable("teacher");

        builder.Property(e => e.TeacherId)
            .UseIdentityAlwaysColumn()
            .HasColumnName("teacher_id");
        builder.Property(e => e.AccountId)
            .HasColumnName("account_id");

        builder.HasOne(d => d.Account)
            .WithMany(p => p.Teachers)
            .HasForeignKey(d => d.AccountId)
            .HasConstraintName("teacher_account_id_fk");
    }
}