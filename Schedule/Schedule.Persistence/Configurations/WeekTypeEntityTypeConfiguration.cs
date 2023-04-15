using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class WeekTypeEntityTypeConfiguration : IEntityTypeConfiguration<WeekType>
{
    public void Configure(EntityTypeBuilder<WeekType> builder)
    {
        builder.HasIndex(e => e.Name, "IX_WeekTypes").IsUnique();
        builder.Property(e => e.Name).HasMaxLength(20);
    }
}