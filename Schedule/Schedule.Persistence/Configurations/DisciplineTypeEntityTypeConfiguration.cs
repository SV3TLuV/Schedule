using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class DisciplineTypeEntityTypeConfiguration
    : IEntityTypeConfiguration<DisciplineType>
{
    public void Configure(EntityTypeBuilder<DisciplineType> builder)
    {
        builder.ToTable("DisciplineType");
        builder.Property(e => e.Name).HasMaxLength(30);
        builder.HasData(new DisciplineType[]
        {
            new ()
            {
                DisciplineTypeId = 1,
                Name = "Дисциплина",
            },
            new ()
            {
                DisciplineTypeId = 2,
                Name = "Практика",
            },
            new ()
            {
                DisciplineTypeId = 3,
                Name = "Внекласная деятельность",
            },
        });
    }
}