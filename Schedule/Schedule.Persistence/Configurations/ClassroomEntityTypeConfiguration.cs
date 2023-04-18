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
        builder.HasMany(e => e.ClassroomClassroomTypes)
            .WithOne(e => e.Classroom)
            .HasForeignKey(e => e.ClassroomId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ClassroomClassroomTypes_Classrooms");
    }
}