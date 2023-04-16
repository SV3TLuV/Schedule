using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class LessonTemplateTeacherClassroomEntityTypeConfiguration 
    : IEntityTypeConfiguration<LessonTemplateTeacherClassroom>
{
    public void Configure(EntityTypeBuilder<LessonTemplateTeacherClassroom> builder)
    {
        builder.HasKey(e => new { e.LessonTemplateId, e.TeacherId });
        builder.HasOne(d => d.Classroom).WithMany(p => p.LessonTemplateTeacherClassrooms)
            .HasForeignKey(d => d.ClassroomId)
            .HasConstraintName("FK_LessonTemplateTeacherClassrooms_Classrooms");
        builder.HasOne(d => d.LessonTemplate).WithMany(p => p.LessonTemplateTeacherClassrooms)
            .HasForeignKey(d => d.LessonTemplateId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_LessonTemplateTeacherClassrooms_LessonTemplates");
        builder.HasOne(d => d.Teacher).WithMany(p => p.LessonTemplateTeacherClassrooms)
            .HasForeignKey(d => d.TeacherId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_LessonTemplateTeacherClassrooms_Teachers");
    }
}