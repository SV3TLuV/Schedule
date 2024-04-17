using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class LessonEntityTypeConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.HasKey(e => e.LessonId)
            .HasName("lesson_pk");

        builder.ToTable("lesson");

        builder.Property(e => e.LessonId)
            .UseIdentityAlwaysColumn()
            .HasColumnName("lesson_id");
        builder.Property(e => e.ClassroomIds)
            .HasColumnName("classroom_ids");
        builder.Property(e => e.DisciplineId)
            .HasColumnName("discipline_id");
        builder.Property(e => e.Number)
            .HasColumnName("number");
        builder.Property(e => e.Subgroup)
            .HasColumnName("subgroup");
        builder.Property(e => e.TeacherIds)
            .HasColumnName("teacher_ids");
        builder.Property(e => e.TimeStart)
            .HasColumnName("time_start");
        builder.Property(e => e.TimeEnd)
            .HasColumnName("time_end");
        builder.Property(e => e.TimetableId)
            .HasColumnName("timetable_id");

        builder.HasOne(d => d.Discipline)
            .WithMany(p => p.Lessons)
            .HasForeignKey(d => d.DisciplineId)
            .HasConstraintName("lesson_discipline_id_fk");

        builder.HasOne(d => d.Timetable)
            .WithMany(p => p.Lessons)
            .HasForeignKey(d => d.TimetableId)
            .HasConstraintName("lesson_timetable_id_fk");
    }
}