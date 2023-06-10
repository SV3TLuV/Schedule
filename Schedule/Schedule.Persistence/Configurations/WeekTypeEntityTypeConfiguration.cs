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
        builder.HasData(new WeekType[]
        {
            new()
            {
                WeekTypeId = 1,
                Name = "Знаменатель",
            },
            new()
            {
                WeekTypeId = 2,
                Name = "Числитель",
            },
        });
    }
}