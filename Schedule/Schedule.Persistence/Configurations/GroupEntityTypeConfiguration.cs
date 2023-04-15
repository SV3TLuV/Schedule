using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class GroupEntityTypeConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.ToTable(tb => tb.HasTrigger("Groups_Delete"));
        builder.HasIndex(e => new { e.Number, e.EnrollmentYear }, "IX_Groups").IsUnique();
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
        builder.HasMany(d => d.GroupId2s).WithMany(p => p.Groups)
            .UsingEntity<Dictionary<string, object>>(
                "GroupGroup",
                r => r.HasOne<Group>().WithMany()
                    .HasForeignKey("GroupId2")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupGroups_Groups1"),
                l => l.HasOne<Group>().WithMany()
                    .HasForeignKey("GroupId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupGroups_Groups"),
                j =>
                {
                    j.HasKey("GroupId", "GroupId2");
                    j.ToTable("GroupGroups");
                });
        builder.HasMany(d => d.Groups).WithMany(p => p.GroupId2s)
            .UsingEntity<Dictionary<string, object>>(
                "GroupGroup",
                r => r.HasOne<Group>().WithMany()
                    .HasForeignKey("GroupId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupGroups_Groups"),
                l => l.HasOne<Group>().WithMany()
                    .HasForeignKey("GroupId2")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupGroups_Groups1"),
                j =>
                {
                    j.HasKey("GroupId", "GroupId2");
                    j.ToTable("GroupGroups");
                });
    }
}