using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class TimeTypeEntityTypeConfiguration : IEntityTypeConfiguration<TimeType>
{
    public void Configure(EntityTypeBuilder<TimeType> builder)
    {
        builder.HasKey(e => e.TimeTypeId)
            .HasName("time_type_pk");

        builder.ToTable("time_type");

        builder.HasIndex(e => e.Name, "time_type_name_index")
            .IsUnique();

        builder.Property(e => e.TimeTypeId)
            .UseIdentityAlwaysColumn()
            .HasColumnName("time_type_id");
        builder.Property(e => e.IsDeleted)
            .HasDefaultValue(false)
            .HasColumnName("is_deleted");
        builder.Property(e => e.Name)
            .HasMaxLength(50)
            .HasColumnName("name");
    }
}