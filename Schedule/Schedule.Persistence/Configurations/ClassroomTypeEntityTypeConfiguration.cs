using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class ClassroomTypeEntityTypeConfiguration : IEntityTypeConfiguration<ClassroomType>
{
    public void Configure(EntityTypeBuilder<ClassroomType> builder)
    {
        builder.HasIndex(e => e.Name, "IX_ClassroomTypes").IsUnique();
        builder.Property(e => e.Name).HasMaxLength(50);
    }
}