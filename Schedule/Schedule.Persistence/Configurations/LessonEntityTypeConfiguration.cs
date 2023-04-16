using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class LessonEntityTypeConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.HasKey(e => e.LessonId).HasName("PK_Pairs");
        builder.HasOne(d => d.Discipline).WithMany(p => p.Lessons)
            .HasForeignKey(d => d.DisciplineId)
            .HasConstraintName("FK_Pairs_Disciplines");
        builder.HasOne(d => d.Time).WithMany(p => p.Lessons)
            .HasForeignKey(d => d.TimeId)
            .HasConstraintName("FK_Pairs_Times");
        builder.HasOne(d => d.Timetable).WithMany(p => p.Lessons)
            .HasForeignKey(d => d.TimetableId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Pairs_Timetables");
    }
}