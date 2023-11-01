using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class GroupEntityTypeConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.HasIndex(e => new { e.Number, e.EnrollmentYear, e.SpecialityId }, "IX_Groups").IsUnique();
        builder.Property(e => e.Number)
            .HasMaxLength(2)
            .IsUnicode(false);
        builder.Property(e => e.IsAfterEleven)
            .HasDefaultValue(false);
        builder.HasOne(d => d.Speciality)
            .WithMany(p => p.Groups)
            .HasForeignKey(d => d.SpecialityId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Groups_Specialities");
        builder.HasOne(d => d.Term)
            .WithMany(p => p.Groups)
            .HasForeignKey(d => d.TermId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Groups_Terms");
        builder.HasMany(e => e.GroupGroups)
            .WithOne(e => e.Group)
            .HasForeignKey(e => e.GroupId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_GroupGroups_Groups");
        builder.HasMany(e => e.GroupGroups1)
            .WithOne(e => e.Group2)
            .HasForeignKey(e => e.GroupId2)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_GroupGroups_Groups1");
    }
}