using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class TimeEntityTypeConfiguration : IEntityTypeConfiguration<Time>
{
    public void Configure(EntityTypeBuilder<Time> builder)
    {
        builder.HasKey(e => e.TimeId)
            .HasName("time_pk");

        builder.ToTable("time");

        builder.HasIndex(e => new
            {
                e.TypeId,
                e.LessonNumber
            }, "time_type_id_lesson_number_index")
            .IsUnique();

        builder.Property(e => e.TimeId)
            .UseIdentityAlwaysColumn()
            .HasColumnName("time_id");
        builder.Property(e => e.Duration)
            .HasColumnName("duration");
        builder.Property(e => e.End)
            .HasColumnName("end");
        builder.Property(e => e.IsDeleted)
            .HasDefaultValue(false)
            .HasColumnName("is_deleted");
        builder.Property(e => e.LessonNumber)
            .HasColumnName("lesson_number");
        builder.Property(e => e.Start)
            .HasColumnName("start");
        builder.Property(e => e.TypeId)
            .HasColumnName("type_id");

        builder.HasOne(d => d.Type)
            .WithMany(p => p.Times)
            .HasForeignKey(d => d.TypeId)
            .HasConstraintName("time_type_id_fk");
    }
}