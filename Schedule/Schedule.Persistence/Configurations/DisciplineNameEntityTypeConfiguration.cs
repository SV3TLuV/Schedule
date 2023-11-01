using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class DisciplineNameEntityTypeConfiguration : IEntityTypeConfiguration<DisciplineName>
{
    public void Configure(EntityTypeBuilder<DisciplineName> builder)
    {
        builder.HasIndex(e => e.Name, "IX_DisciplineNames").IsUnique();
        builder.Property(e => e.Name).HasMaxLength(50);
    }
}