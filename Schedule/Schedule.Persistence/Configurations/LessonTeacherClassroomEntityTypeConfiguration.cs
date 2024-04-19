using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public class LessonTeacherClassroomEntityTypeConfiguration : IEntityTypeConfiguration<LessonTeacherClassroom>
{
    public void Configure(EntityTypeBuilder<LessonTeacherClassroom> builder)
    {
        builder.HasKey(e => new { e.LessonId, e.TeacherId, e.ClassroomId })
            .HasName("lesson_teacher_classroom_pk");

        builder.ToTable("lesson_teacher_classroom");

        builder.Property(e => e.LessonId)
            .HasColumnName("lesson_id");
        builder.Property(e => e.TeacherId)
            .HasColumnName("teacher_id");
        builder.Property(e => e.ClassroomId)
            .HasColumnName("classroom_id");

        builder.HasOne(d => d.Lesson)
            .WithMany(p => p.LessonTeacherClassrooms)
            .HasForeignKey(d => d.LessonId)
            .HasConstraintName("lesson_teacher_classroom_lesson_id_fk");

        builder.HasOne(d => d.Teacher)
            .WithMany(p => p.LessonTeacherClassrooms)
            .HasForeignKey(d => d.TeacherId)
            .HasConstraintName("lesson_teacher_classroom_teacher_id_fk");

        builder.HasOne(d => d.Classroom)
            .WithMany(p => p.LessonTeacherClassrooms)
            .HasForeignKey(d => d.ClassroomId)
            .HasConstraintName("lesson_teacher_classroom_classroom_id_fk");
    }
}