using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class DayEntityTypeConfiguration : IEntityTypeConfiguration<Day>
{
    public void Configure(EntityTypeBuilder<Day> builder)
    {
        builder.HasIndex(e => e.Name, "IX_Days").IsUnique();
        builder.Property(e => e.Name).HasMaxLength(20);
    }
}