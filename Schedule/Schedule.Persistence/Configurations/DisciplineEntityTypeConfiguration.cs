using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class DisciplineEntityTypeConfiguration : IEntityTypeConfiguration<Discipline>
{
    public void Configure(EntityTypeBuilder<Discipline> builder)
    {
        builder.HasIndex(e => new { e.CodeId, e.NameId, e.SpecialityId, e.TermId }, "IX_Disciplines").IsUnique();
        builder.HasOne(d => d.DisciplineType)
            .WithMany(p => p.Disciplines)
            .HasForeignKey(d => d.DisciplineTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Disciplines_DisciplineType");
        builder.HasOne(d => d.Speciality)
            .WithMany(p => p.Disciplines)
            .HasForeignKey(d => d.SpecialityId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .HasConstraintName("FK_Disciplines_Specialities");
        builder.HasOne(d => d.Term)
            .WithMany(p => p.Disciplines)
            .HasForeignKey(d => d.TermId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Disciplines_Terms");
        builder.HasOne(d => d.Name)
            .WithMany(p => p.Disciplines)
            .HasForeignKey(d => d.NameId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Disciplines_DisciplineNames");
        builder.HasOne(d => d.Code)
            .WithMany(p => p.Disciplines)
            .HasForeignKey(d => d.CodeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Disciplines_DisciplineCodes");
    }
}