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
    }
}