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
        builder.HasData(new TimeType[]
        {
            new()
            {
                TimeTypeId = 1,
                Name = "Стандартное",
            },
            new()
            {
                TimeTypeId = 2,
                Name = "Сокращенное",
            },
            new()
            {
                TimeTypeId = 3,
                Name = "Понедельник",
            },
        });
    }
}