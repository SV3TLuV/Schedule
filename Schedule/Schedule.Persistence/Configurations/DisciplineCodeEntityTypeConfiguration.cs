using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class DisciplineCodeEntityTypeConfiguration : IEntityTypeConfiguration<DisciplineCode>
{
    public void Configure(EntityTypeBuilder<DisciplineCode> builder)
    {
        builder.HasIndex(e => e.Code, "IX_DisciplineCodes").IsUnique();
        builder.Property(e => e.Code).HasMaxLength(20);
        builder.HasData(new DisciplineCode[]
        {
            new()
            {
                DisciplineCodeId = 1,
                Code = "ЕН.01",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 2,
                Code = "ЕН.02",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 3,
                Code = "ЕН.03",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 4,
                Code = "ОГСЭ.01",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 5,
                Code = "ОГСЭ.02",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 6,
                Code = "ОГСЭ.03",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 7,
                Code = "ОГСЭ.04",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 8,
                Code = "ОГСЭ.05",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 9,
                Code = "ОП.01",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 10,
                Code = "ОП.02",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 11,
                Code = "ОП.03",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 12,
                Code = "ОП.04",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 13,
                Code = "ОП.05",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 14,
                Code = "ОП.06",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 15,
                Code = "ОП.07",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 16,
                Code = "ОП.08",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 17,
                Code = "ОП.09",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 18,
                Code = "ОП.10",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 19,
                Code = "ОП.11",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 20,
                Code = "ОП.12",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 21,
                Code = "ОП.13",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 22,
                Code = "ОП.14",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 23,
                Code = "ОУД.01",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 24,
                Code = "ОУД.02",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 25,
                Code = "ОУД.03",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 26,
                Code = "ОУД.04",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 27,
                Code = "ОУД.05",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 28,
                Code = "ОУД.06",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 29,
                Code = "ОУД.07",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 30,
                Code = "ОУД.08",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 31,
                Code = "ОУД.09",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 32,
                Code = "ОУД.10",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 33,
                Code = "ОУД.11",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 34,
                Code = "ОУД.12",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 35,
                Code = "ОУД.13",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 36,
                Code = "СГ.01",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 37,
                Code = "СГ.02",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 38,
                Code = "СГ.04",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 39,
                Code = "СГ.05",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 40,
                Code = "СГ.06",
                IsDeleted = false,
            },
            new()
            {
                DisciplineCodeId = 41,
                Code = "СГ.07",
                IsDeleted = false,
            }
        });
    }
}