using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class TeacherGroupEntityTypeConfiguration
    : IEntityTypeConfiguration<TeacherGroup>
{
    public void Configure(EntityTypeBuilder<TeacherGroup> builder)
    {
        builder.ToTable("TeacherGroups");
        builder.HasKey(e => new { e.TeacherId, e.GroupId })
            .HasName("PK_TeacherGroups");
        builder.HasOne(e => e.Teacher)
            .WithMany(e => e.TeacherGroups)
            .HasForeignKey(e => e.TeacherId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_TeacherGroups_Teachers");
        builder.HasOne(e => e.Group)
            .WithMany(e => e.TeacherGroups)
            .HasForeignKey(e => e.GroupId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_TeacherGroups_Groups");
    }
}