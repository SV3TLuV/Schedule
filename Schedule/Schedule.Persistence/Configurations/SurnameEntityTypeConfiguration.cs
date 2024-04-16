using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class SurnameEntityTypeConfiguration : IEntityTypeConfiguration<Surname>
{
    public void Configure(EntityTypeBuilder<Surname> builder)
    {
        builder.HasKey(e => e.Value)
            .HasName("surname_pk");

        builder.ToTable("surname");

        builder.Property(e => e.Value)
            .HasMaxLength(40)
            .HasColumnName("surname");
    }
}