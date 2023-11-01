using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class SpecialityEntityTypeConfiguration : IEntityTypeConfiguration<Speciality>
{
    public void Configure(EntityTypeBuilder<Speciality> builder)
    {
        builder.HasIndex(e => new { e.Code, e.Name }, "IX_Specialities").IsUnique();
        builder.Property(e => e.Code).HasMaxLength(20);
        builder.Property(e => e.Name).HasMaxLength(20);
        builder.HasOne(d => d.MaxTerm)
            .WithMany(p => p.Specialities)
            .HasForeignKey(d => d.MaxTermId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Specialities_Terms");
    }
}