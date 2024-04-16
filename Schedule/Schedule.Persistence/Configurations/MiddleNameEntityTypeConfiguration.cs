using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class MiddleNameEntityTypeConfiguration : IEntityTypeConfiguration<MiddleName>
{
    public void Configure(EntityTypeBuilder<MiddleName> builder)
    {
        builder.HasKey(e => e.Value)
            .HasName("middle_name_pk");

        builder.ToTable("middle_name");

        builder.Property(e => e.Value)
            .HasMaxLength(40)
            .HasColumnName("middle_name");
    }
}