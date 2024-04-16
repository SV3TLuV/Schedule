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
                DisciplineCodeId = e.CodeId,
                DisciplineNameId = e.NameId,
                e.SpecialityId,
                e.TermId
            }, "discipline_index")
            .IsUnique();

        builder.Property(e => e.DisciplineId)
            .UseIdentityAlwaysColumn()
            .HasColumnName("discipline_id");
        builder.Property(e => e.CodeId)
            .HasColumnName("discipline_code_id");
        builder.Property(e => e.NameId)
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

        builder.HasOne(d => d.Code)
            .WithMany(p => p.Disciplines)
            .HasForeignKey(d => d.CodeId)
            .HasConstraintName("discipline_code_id_fk");

        builder.HasOne(d => d.Name)
            .WithMany(p => p.Disciplines)
            .HasForeignKey(d => d.NameId)
            .HasConstraintName("discipline_name_id_fk");

        builder.HasOne(d => d.Type)
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