using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class DisciplineEntityTypeConfiguration : IEntityTypeConfiguration<Discipline>
{
    public void Configure(EntityTypeBuilder<Discipline> builder)
    {
        builder.HasKey(e => e.DisciplineId)
            .HasName("discipline_pk");

        builder.ToTable("discipline");

        builder.HasIndex(e => new
            {
                e.DisciplineCodeId,
                e.DisciplineNameId,
                e.SpecialityId,
                e.TermId
            }, "discipline_index")
            .IsUnique();

        builder.Property(e => e.DisciplineId)
            .UseIdentityAlwaysColumn()
            .HasColumnName("discipline_id");
        builder.Property(e => e.DisciplineCodeId)
            .HasColumnName("discipline_code_id");
        builder.Property(e => e.DisciplineNameId)
            .HasColumnName("discipline_name_id");
        builder.Property(e => e.DisciplineTypeId)
            .HasColumnName("discipline_type_id");
        builder.Property(e => e.IsDeleted)
            .HasDefaultValue(false)
            .HasColumnName("is_deleted");
        builder.Property(e => e.SpecialityId)
            .HasColumnName("speciality_id");
        builder.Property(e => e.TermId)
            .HasColumnName("term_id");
        builder.Property(e => e.TotalHours)
            .HasColumnName("total_hours");

        builder.HasOne(d => d.DisciplineCode)
            .WithMany(p => p.Disciplines)
            .HasForeignKey(d => d.DisciplineCodeId)
            .HasConstraintName("discipline_code_id_fk");

        builder.HasOne(d => d.DisciplineName)
            .WithMany(p => p.Disciplines)
            .HasForeignKey(d => d.DisciplineNameId)
            .HasConstraintName("discipline_name_id_fk");

        builder.HasOne(d => d.DisciplineType)
            .WithMany(p => p.Disciplines)
            .HasForeignKey(d => d.DisciplineTypeId)
            .HasConstraintName("discipline_type_id_fk");

        builder.HasOne(d => d.Speciality)
            .WithMany(p => p.Disciplines)
            .HasForeignKey(d => d.SpecialityId)
            .HasConstraintName("discipline_speciality_id_fk");

        builder.HasOne(d => d.Term)
            .WithMany(p => p.Disciplines)
            .HasForeignKey(d => d.TermId)
            .HasConstraintName("discipline_term_id_fk");
    }
}