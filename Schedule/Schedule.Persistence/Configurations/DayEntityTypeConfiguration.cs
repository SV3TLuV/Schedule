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
        builder.HasData(new Day[]
        {
            new()
            {
                DayId = 1,
                Name = "Понедельник",
                IsStudy = true,
            },
            new()
            {
                DayId = 2,
                Name = "Вторник",
                IsStudy = true,
            },
            new()
            {
                DayId = 3,
                Name = "Среда",
                IsStudy = true,
            },
            new()
            {
                DayId = 4,
                Name = "Четверг",
                IsStudy = true,
            },
            new()
            {
                DayId = 5,
                Name = "Пятница",
                IsStudy = true,
            },
            new()
            {
                DayId = 6,
                Name = "Суббота",
                IsStudy = true,
            },
            new()
            {
                DayId = 7,
                Name = "Воскресенье",
                IsStudy = false,
            }
        });
    }
}