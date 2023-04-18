using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class TeacherDisciplineEntityTypeConfiguration
    : IEntityTypeConfiguration<TeacherDiscipline>
{
    public void Configure(EntityTypeBuilder<TeacherDiscipline> builder)
    {
        builder.ToTable("TeacherDisciplines");
        builder.HasKey(e => new { e.TeacherId, e.DisciplineId })
            .HasName("PK_TeacherDisciplines");
    }
}