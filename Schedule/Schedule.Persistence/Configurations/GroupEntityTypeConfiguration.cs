using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class GroupEntityTypeConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.ToTable(tb =>
            {
                tb.HasTrigger("Groups_Delete");
                tb.HasTrigger("Groups_Insert");
            });
        builder.HasIndex(e => new { e.Number, e.EnrollmentYear, e.SpecialityCodeId }, "IX_Groups").IsUnique();
        builder.Property(e => e.Number)
            .HasMaxLength(2)
            .IsUnicode(false);
        builder.HasOne(d => d.Course).WithMany(p => p.Groups)
            .HasForeignKey(d => d.CourseId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Groups_Courses");
        builder.HasOne(d => d.SpecialityCode).WithMany(p => p.Groups)
            .HasForeignKey(d => d.SpecialityCodeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Groups_SpecialityCodes");
        builder.HasMany(e => e.TeacherGroups)
            .WithOne(e => e.Group)
            .HasForeignKey(e => e.GroupId)
            .HasConstraintName("FK_TeacherGroups_Groups");
        builder.HasMany(e => e.GroupGroups)
            .WithOne(e => e.Group)
            .HasForeignKey(e => e.GroupId)
            .HasConstraintName("FK_GroupGroups_Groups");
        builder.HasMany(e => e.GroupGroups1)
            .WithOne(e => e.Group1)
            .HasForeignKey(e => e.GroupId1)
            .HasConstraintName("FK_GroupGroups_Groups1");
    }
}