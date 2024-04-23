using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class GroupEntityTypeConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.HasKey(e => e.GroupId)
            .HasName("group_pk");

        builder.ToTable("group");

        builder.HasIndex(e => new
            {
                e.Number,
                e.EnrollmentYear,
                e.SpecialityId
            }, "group_index")
            .IsUnique();

        builder.Property(e => e.GroupId)
            .UseIdentityAlwaysColumn()
            .HasColumnName("group_id");
        builder.Property(e => e.Name)
            .HasMaxLength(20)
            .HasColumnName("name");
        builder.Property(e => e.EnrollmentYear)
            .HasColumnName("enrollment_year");
        builder.Property(e => e.IsDeleted)
            .HasDefaultValue(false)
            .HasColumnName("is_deleted");
        builder.Property(e => e.Number)
            .HasMaxLength(2)
            .HasColumnName("number");
        builder.Property(e => e.SpecialityId)
            .HasColumnName("speciality_id");
        builder.Property(e => e.TermId)
            .HasColumnName("term_id");
        builder.Property(e => e.IsAfterEleven)
            .HasColumnName("is_after_eleven");

        builder.HasOne(d => d.Speciality)
            .WithMany(p => p.Groups)
            .HasForeignKey(d => d.SpecialityId)
            .HasConstraintName("group_speciality_id_fk");

        builder.HasOne(d => d.Term)
            .WithMany(p => p.Groups)
            .HasForeignKey(d => d.TermId)
            .HasConstraintName("group_term_id_fk");
    }
}