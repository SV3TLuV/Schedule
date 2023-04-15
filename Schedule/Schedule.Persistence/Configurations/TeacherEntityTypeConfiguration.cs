using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class TeacherEntityTypeConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.ToTable(tb => tb.HasTrigger("Teachers_Delete"));
        builder.HasIndex(e => e.Email, "IX_Teachers").IsUnique();
        builder.Property(e => e.Email).HasMaxLength(200);
        builder.Property(e => e.MiddleName).HasMaxLength(40);
        builder.Property(e => e.Name).HasMaxLength(40);
        builder.Property(e => e.Surname).HasMaxLength(40);
        builder.HasMany(d => d.Disciplines).WithMany(p => p.Teachers)
            .UsingEntity<Dictionary<string, object>>(
                "TeacherDiscipline",
                r => r.HasOne<Discipline>().WithMany()
                    .HasForeignKey("DisciplineId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeacherDisciplines_Disciplines"),
                l => l.HasOne<Teacher>().WithMany()
                    .HasForeignKey("TeacherId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeacherDisciplines_Teachers"),
                j =>
                {
                    j.HasKey("TeacherId", "DisciplineId");
                    j.ToTable("TeacherDisciplines");
                });
        builder.HasMany(d => d.Groups).WithMany(p => p.Teachers)
            .UsingEntity<Dictionary<string, object>>(
                "TeacherGroup",
                r => r.HasOne<Group>().WithMany()
                    .HasForeignKey("GroupId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeacherGroups_Groups"),
                l => l.HasOne<Teacher>().WithMany()
                    .HasForeignKey("TeacherId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeacherGroups_Teachers"),
                j =>
                {
                    j.HasKey("TeacherId", "GroupId");
                    j.ToTable("TeacherGroups");
                });
    }
}