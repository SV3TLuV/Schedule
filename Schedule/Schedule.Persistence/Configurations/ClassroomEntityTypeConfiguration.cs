using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class ClassroomEntityTypeConfiguration : IEntityTypeConfiguration<Classroom>
{
    public void Configure(EntityTypeBuilder<Classroom> builder)
    {
        builder.HasKey(e => e.ClassroomId)
            .HasName("classroom_pk");

        builder.ToTable("classroom");

        builder.HasIndex(e => e.Cabinet, "classroom_cabinet_index")
            .IsUnique();

        builder.Property(e => e.ClassroomId)
            .UseIdentityAlwaysColumn()
            .HasColumnName("classroom_id");
        builder.Property(e => e.Cabinet)
            .HasMaxLength(10)
            .HasColumnName("cabinet");
        builder.Property(e => e.IsDeleted)
            .HasDefaultValue(false)
            .HasColumnName("is_deleted");
    }
}