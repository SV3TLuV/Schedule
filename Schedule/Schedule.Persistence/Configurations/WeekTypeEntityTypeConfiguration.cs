using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class WeekTypeEntityTypeConfiguration : IEntityTypeConfiguration<WeekType>
{
    public void Configure(EntityTypeBuilder<WeekType> builder)
    {
        builder.HasKey(e => e.WeekTypeId)
            .HasName("week_type_pk");

        builder.ToTable("week_type");

        builder.HasIndex(e => e.Name, "week_type_name_index")
            .IsUnique();

        builder.Property(e => e.WeekTypeId)
            .UseIdentityAlwaysColumn()
            .HasColumnName("week_type_id");
        builder.Property(e => e.Name)
            .HasMaxLength(20)
            .HasColumnName("name");
    }
}