using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class ClassroomEntityTypeConfiguration : IEntityTypeConfiguration<Classroom>
{
    public void Configure(EntityTypeBuilder<Classroom> builder)
    {
        builder.ToTable(tb => tb.HasTrigger("Classrooms_Delete"));
        builder.HasIndex(e => e.Cabinet, "IX_Classrooms").IsUnique();
        builder.Property(e => e.Cabinet).HasMaxLength(10);
        builder.HasMany(d => d.ClassroomTypes).WithMany(p => p.Classrooms)
            .UsingEntity<Dictionary<string, object>>(
                "ClassroomClassroomType",
                r => r.HasOne<ClassroomType>().WithMany()
                    .HasForeignKey("ClassroomTypeId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClassroomClassroomTypes_ClassroomTypes"),
                l => l.HasOne<Classroom>().WithMany()
                    .HasForeignKey("ClassroomId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClassroomClassroomTypes_Classrooms"),
                j =>
                {
                    j.HasKey("ClassroomId", "ClassroomTypeId");
                    j.ToTable("ClassroomClassroomTypes");
                });
    }
}