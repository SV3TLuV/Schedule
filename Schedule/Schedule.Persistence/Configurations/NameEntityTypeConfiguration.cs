using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class NameEntityTypeConfiguration : IEntityTypeConfiguration<Name>
{
    public void Configure(EntityTypeBuilder<Name> builder)
    {
        builder.HasKey(e => e.Value)
            .HasName("name_pk");

        builder.ToTable("name");

        builder.Property(e => e.Value)
            .HasMaxLength(40)
            .HasColumnName("name");
    }
}