using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class StudentEntityTypeConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(e => e.StudentId)
            .HasName("student_pk");

        builder.ToTable("student");

        builder.Property(e => e.StudentId)
            .UseIdentityAlwaysColumn()
            .HasColumnName("student_id");
        builder.Property(e => e.AccountId)
            .HasColumnName("account_id");
        builder.Property(e => e.GroupId)
            .HasColumnName("group_id");

        builder.HasOne(d => d.Account)
            .WithMany(p => p.Students)
            .HasForeignKey(d => d.AccountId)
            .HasConstraintName("student_account_id_fk");

        builder.HasOne(d => d.Group)
            .WithMany(p => p.Students)
            .HasForeignKey(d => d.GroupId)
            .HasConstraintName("student_group_id_fk");
    }
}