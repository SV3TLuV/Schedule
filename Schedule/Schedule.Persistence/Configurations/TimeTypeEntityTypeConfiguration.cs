using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class TimeTypeEntityTypeConfiguration : IEntityTypeConfiguration<TimeType>
{
    public void Configure(EntityTypeBuilder<TimeType> builder)
    {
        builder.ToTable(tb => tb.HasTrigger("TimeTypes_Delete"));
        builder.HasIndex(e => e.Name, "IX_TimeTypes").IsUnique();
        builder.Property(e => e.Name).HasMaxLength(50);
    }
}