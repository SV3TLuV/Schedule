using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class LessonTeacherClassroomEntityTypeConfiguration
    : IEntityTypeConfiguration<LessonTeacherClassroom>
{
    public void Configure(EntityTypeBuilder<LessonTeacherClassroom> builder)
    {
        builder.HasKey(e => new { e.LessonId, e.TeacherId }).HasName("PK_LessonTeachers");
        builder.HasOne(d => d.Classroom)
            .WithMany(p => p.LessonTeacherClassrooms)
            .HasForeignKey(d => d.ClassroomId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .HasConstraintName("FK_LessonTeacherClassrooms_Classrooms");
        builder.HasOne(d => d.Lesson)
            .WithMany(p => p.LessonTeacherClassrooms)
            .HasForeignKey(d => d.LessonId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .HasConstraintName("FK_LessonTeacherClassrooms_Lessons");
        builder.HasOne(d => d.Teacher)
            .WithMany(p => p.LessonTeacherClassrooms)
            .HasForeignKey(d => d.TeacherId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .HasConstraintName("FK_LessonTeacherClassrooms_Teachers");
    }
}