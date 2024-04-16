using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class DayEntityTypeConfiguration : IEntityTypeConfiguration<Day>
{
    public void Configure(EntityTypeBuilder<Day> builder)
    {
        builder.HasKey(e => e.DayId)
            .HasName("day_pk");

        builder.ToTable("day");

        builder.HasIndex(e => e.Name, "day_name_index")
            .IsUnique();

        builder.Property(e => e.DayId)
            .UseIdentityAlwaysColumn()
            .HasColumnName("day_id");
        builder.Property(e => e.Name)
            .HasMaxLength(20)
            .HasColumnName("name");
    }
}