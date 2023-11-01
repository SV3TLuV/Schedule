using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class TeacherEntityTypeConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.HasIndex(e => e.Email, "IX_Teachers").IsUnique();
        builder.Property(e => e.Email).HasMaxLength(200);
        builder.Property(e => e.MiddleName).HasMaxLength(40);
        builder.Property(e => e.Name).HasMaxLength(40);
        builder.Property(e => e.Surname).HasMaxLength(40);
    }
}