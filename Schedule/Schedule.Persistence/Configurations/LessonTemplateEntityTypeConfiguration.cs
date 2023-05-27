using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class LessonTemplateEntityTypeConfiguration : IEntityTypeConfiguration<LessonTemplate>
{
    public void Configure(EntityTypeBuilder<LessonTemplate> builder)
    {
        builder.HasOne(d => d.Discipline).WithMany(p => p.LessonTemplates)
            .HasForeignKey(d => d.DisciplineId)
            .HasConstraintName("FK_LessonTemplates_Disciplines");
        builder.HasOne(d => d.Template).WithMany(p => p.LessonTemplates)
            .HasForeignKey(d => d.TemplateId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_LessonTemplates_Templates");
        builder.HasOne(d => d.Time).WithMany(p => p.LessonTemplates)
            .HasForeignKey(d => d.TimeId)
            .HasConstraintName("FK_LessonTemplates_Times");
    }
}