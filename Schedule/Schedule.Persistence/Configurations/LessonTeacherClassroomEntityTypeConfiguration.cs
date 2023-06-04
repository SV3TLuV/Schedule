using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class LessonTeacherClassroomEntityTypeConfiguration
    : IEntityTypeConfiguration<LessonTeacherClassroom>
{
    public void Configure(EntityTypeBuilder<LessonTeacherClassroom> builder)
    {
        builder.HasKey(e => new { e.LessonId, e.TeacherId }).HasName("PK_PairTeachers");
        builder.HasOne(d => d.Classroom).WithMany(p => p.LessonTeacherClassrooms)
            .HasForeignKey(d => d.ClassroomId)
            .HasConstraintName("FK_LessonTeacherClassrooms_Classrooms2");
        builder.HasOne(d => d.Lesson).WithMany(p => p.LessonTeacherClassrooms)
            .HasForeignKey(d => d.LessonId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_LessonTeacherClassrooms_Lessons");
        builder.HasOne(d => d.Teacher).WithMany(p => p.LessonTeacherClassrooms)
            .HasForeignKey(d => d.TeacherId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_LessonTeacherClassrooms_Teachers1");
    }
}