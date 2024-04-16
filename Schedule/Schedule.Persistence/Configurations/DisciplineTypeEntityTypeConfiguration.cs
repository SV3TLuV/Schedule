using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class DisciplineTypeEntityTypeConfiguration : IEntityTypeConfiguration<DisciplineType>
{
    public void Configure(EntityTypeBuilder<DisciplineType> builder)
    {
        builder.HasKey(e => e.DisciplineTypeId)
            .HasName("discipline_type_pk");

        builder.ToTable("discipline_type");

        builder.Property(e => e.DisciplineTypeId)
            .UseIdentityAlwaysColumn()
            .HasColumnName("discipline_type_id");
        builder.Property(e => e.Name)
            .HasMaxLength(30)
            .HasColumnName("name");
    }
}