using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class TimetableEntityTypeConfiguration : IEntityTypeConfiguration<Timetable>
{
    public void Configure(EntityTypeBuilder<Timetable> builder)
    {
        builder.HasIndex(e => new { e.DateId, e.GroupId }, "IX_Timetables").IsUnique();
        builder.HasIndex(e => e.DateId, "IX_Timetables_DateId");
        builder.HasIndex(e => e.GroupId, "IX_Timetables_GroupId");
        builder.HasOne(d => d.Date).WithMany(p => p.Timetables)
            .HasForeignKey(d => d.DateId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Timetables_Dates");
        builder.HasOne(d => d.Group).WithMany(p => p.Timetables)
            .HasForeignKey(d => d.GroupId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Timetables_Groups");
    }
}