using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class DisciplineEntityTypeConfiguration : IEntityTypeConfiguration<Discipline>
{
    public void Configure(EntityTypeBuilder<Discipline> builder)
    {
        builder.ToTable(tb => tb.HasTrigger("Disciplines_Delete"));
        builder.HasIndex(e => new { e.Code, e.Name, e.SpecialityId, e.TermId }, "IX_Disciplines").IsUnique();
        builder.Property(e => e.Code).HasMaxLength(20);
        builder.Property(e => e.Name).HasMaxLength(50);
        builder.HasOne(d => d.DisciplineType)
            .WithMany(p => p.Disciplines)
            .HasForeignKey(d => d.DisciplineTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Disciplines_DisciplineType");
        builder.HasOne(d => d.Speciality).WithMany(p => p.Disciplines)
            .HasForeignKey(d => d.SpecialityId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Disciplines_Specialities");
        builder.HasOne(d => d.Term).WithMany(p => p.Disciplines)
            .HasForeignKey(d => d.TermId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Disciplines_Terms");
    }
}