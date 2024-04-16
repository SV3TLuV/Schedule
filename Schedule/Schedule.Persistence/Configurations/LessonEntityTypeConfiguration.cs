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
        builder.Property(e => e.LessonChangeId)
            .HasColumnName("lesson_change_id");
        builder.Property(e => e.Number)
            .HasColumnName("number");
        builder.Property(e => e.Subgroup)
            .HasColumnName("subgroup");
        builder.Property(e => e.TeacherIds)
            .HasColumnName("teacher_ids");
        builder.Property(e => e.TimeId)
            .HasColumnName("time_id");
        builder.Property(e => e.TimetableId)
            .HasColumnName("timetable_id");

        builder.HasOne(d => d.Discipline)
            .WithMany(p => p.Lessons)
            .HasForeignKey(d => d.DisciplineId)
            .HasConstraintName("lesson_discipline_id_fk");

        builder.HasOne(d => d.LessonChange)
            .WithMany(p => p.Lessons)
            .HasForeignKey(d => d.LessonChangeId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("lesson_lesson_change_id_fk");

        builder.HasOne(d => d.Time)
            .WithMany(p => p.Lessons)
            .HasForeignKey(d => d.TimeId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("lesson_time_id_fk");

        builder.HasOne(d => d.Timetable)
            .WithMany(p => p.Lessons)
            .HasForeignKey(d => d.TimetableId)
            .HasConstraintName("lesson_timetable_id_fk");
    }
}