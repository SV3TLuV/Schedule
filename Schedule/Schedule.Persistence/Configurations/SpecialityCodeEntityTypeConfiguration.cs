using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class SpecialityCodeEntityTypeConfiguration : IEntityTypeConfiguration<SpecialityCode>
{
    public void Configure(EntityTypeBuilder<SpecialityCode> builder)
    {
        builder.ToTable(tb => tb.HasTrigger("SpecialityCodes_Delete"));
        builder.HasIndex(e => new { e.Code, e.Name }, "IX_SpecialityCodes").IsUnique();
        builder.Property(e => e.Code).HasMaxLength(20);
        builder.Property(e => e.Name).HasMaxLength(20);
    }
}