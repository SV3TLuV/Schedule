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
    }
}