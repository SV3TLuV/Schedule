using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class SpecialityEntityTypeConfiguration : IEntityTypeConfiguration<Speciality>
{
    public void Configure(EntityTypeBuilder<Speciality> builder)
    {
        builder.ToTable(tb => tb.HasTrigger("Specialities_Delete"));
        builder.HasIndex(e => new { e.Code, e.Name }, "IX_Specialities").IsUnique();
        builder.Property(e => e.Code).HasMaxLength(20);
        builder.Property(e => e.Name).HasMaxLength(20);
    }
}