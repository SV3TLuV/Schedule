using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class LessonChangeEntityTypeConfiguration : IEntityTypeConfiguration<LessonChange>
{
    public void Configure(EntityTypeBuilder<LessonChange> builder)
    {
        builder.HasKey(e => e.LessonChangeId)
            .HasName("lesson_change_pk");

        builder.ToTable("lesson_change");

        builder.Property(e => e.LessonChangeId)
            .UseIdentityAlwaysColumn()
            .HasColumnName("lesson_change_id");
        builder.Property(e => e.ClassroomIds)
            .HasColumnName("classroom_ids");
        builder.Property(e => e.Date)
            .HasColumnName("date");
        builder.Property(e => e.DisciplineId)
            .HasColumnName("discipline_id");
        builder.Property(e => e.LessonId)
            .HasColumnName("lesson_id");
        builder.Property(e => e.Number)
            .HasColumnName("number");
        builder.Property(e => e.Subgroup)
            .HasColumnName("subgroup");
        builder.Property(e => e.TeacherIds)
            .HasColumnName("teacher_ids");
        builder.Property(e => e.TimeId)
            .HasColumnName("time_id");

        builder.HasOne(d => d.Discipline)
            .WithMany(p => p.LessonChanges)
            .HasForeignKey(d => d.DisciplineId)
            .HasConstraintName("lesson_change_discipline_id_fk");

        builder.HasOne(d => d.Lesson)
            .WithMany(p => p.LessonChanges)
            .HasForeignKey(d => d.LessonId)
            .HasConstraintName("lesson_change_lesson_id_fk");

        builder.HasOne(d => d.Time)
            .WithMany(p => p.LessonChanges)
            .HasForeignKey(d => d.TimeId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("lesson_change_time_id_fk");
    }
}