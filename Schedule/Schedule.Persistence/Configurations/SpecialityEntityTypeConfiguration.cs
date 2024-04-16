using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class SpecialityEntityTypeConfiguration : IEntityTypeConfiguration<Speciality>
{
    public void Configure(EntityTypeBuilder<Speciality> builder)
    {
        builder.HasKey(e => e.SpecialityId)
            .HasName("speciality_pk");

        builder.ToTable("speciality");

        builder.HasIndex(e => e.Name, "speciality_name_index")
            .IsUnique();

        builder.Property(e => e.SpecialityId)
            .UseIdentityAlwaysColumn()
            .HasColumnName("speciality_id");
        builder.Property(e => e.Code)
            .HasMaxLength(20)
            .HasColumnName("code");
        builder.Property(e => e.IsDeleted)
            .HasDefaultValue(false)
            .HasColumnName("is_deleted");
        builder.Property(e => e.MaxTermId)
            .HasColumnName("max_term_id");
        builder.Property(e => e.Name)
            .HasMaxLength(20)
            .HasColumnName("name");

        builder.HasOne(d => d.MaxTerm)
            .WithMany(p => p.Specialities)
            .HasForeignKey(d => d.MaxTermId)
            .HasConstraintName("speciality_max_term_id_fk");
    }
}