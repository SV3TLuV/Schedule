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
        builder.HasData(new Speciality[]
        {
            new() { SpecialityId = 1, Code = "09.02.01", Name = "КСК", MaxTermId = 8, IsDeleted = false, },
            new() { SpecialityId = 2, Code = "09.02.03 ", Name = "ПКС", MaxTermId = 8, IsDeleted = false, },
            new() { SpecialityId = 3, Code = "09.02.06", Name = "ССА", MaxTermId = 8, IsDeleted = false, },
            new() { SpecialityId = 4, Code = "09.02.07 ", Name = "ИСПП", MaxTermId = 8, IsDeleted = false, },
            new() { SpecialityId = 5, Code = "09.02.07 ", Name = "ИСПВ", MaxTermId = 8, IsDeleted = false, },
            new() { SpecialityId = 6, Code = "10.02.04", Name = "ОИБ", MaxTermId = 8, IsDeleted = false, },
            new() { SpecialityId = 7, Code = "11.02.15", Name = "ИСС", MaxTermId = 8, IsDeleted = false, },
            new() { SpecialityId = 8, Code = "11.02.18", Name = "РМТ", MaxTermId = 8, IsDeleted = false, },
            new() { SpecialityId = 9, Code = "11.02.10", Name = "Р", MaxTermId = 8, IsDeleted = false, },
            new() { SpecialityId = 10, Code = "11.02.11", Name = "С", MaxTermId = 8, IsDeleted = false, },
        });
    }
}