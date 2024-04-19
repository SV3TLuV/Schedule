using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public class LessonChangeTeacherClassroomEntityTypeConfiguration : IEntityTypeConfiguration<LessonChangeTeacherClassroom>
{
    public void Configure(EntityTypeBuilder<LessonChangeTeacherClassroom> builder)
    {
        builder.HasKey(e => new { e.LessonChangeId, e.TeacherId, e.ClassroomId })
            .HasName("lesson_change_teacher_classroom_pk");

        builder.ToTable("lesson_change_teacher_classroom");

        builder.Property(e => e.LessonChangeId)
            .HasColumnName("lesson_change_id");
        builder.Property(e => e.TeacherId)
            .HasColumnName("teacher_id");
        builder.Property(e => e.ClassroomId)
            .HasColumnName("classroom_id");

        builder.HasOne(d => d.LessonChange)
            .WithMany(p => p.LessonChangeTeacherClassrooms)
            .HasForeignKey(d => d.LessonChangeId)
            .HasConstraintName("lesson_change_teacher_classroom_lesson_change_id_fk");

        builder.HasOne(d => d.Teacher)
            .WithMany(p => p.LessonChangeTeacherClassrooms)
            .HasForeignKey(d => d.TeacherId)
            .HasConstraintName("lesson_change_teacher_classroom_teacher_id_fk");

        builder.HasOne(d => d.Classroom)
            .WithMany(p => p.LessonChangeTeacherClassrooms)
            .HasForeignKey(d => d.ClassroomId)
            .HasConstraintName("lesson_change_teacher_classroom_classroom_id_fk");
    }
}