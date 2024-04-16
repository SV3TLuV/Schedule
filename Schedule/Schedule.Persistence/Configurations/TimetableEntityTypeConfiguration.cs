using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class TimetableEntityTypeConfiguration : IEntityTypeConfiguration<Timetable>
{
    public void Configure(EntityTypeBuilder<Timetable> builder)
    {
        builder.HasKey(e => e.TimetableId)
            .HasName("timetable_pk");

        builder.ToTable("timetable");

        builder.HasIndex(e => new
            {
                e.Created,
                e.GroupId
            }, "timetable_created_group_id_index")
            .IsUnique();

        builder.HasIndex(e => e.GroupId, "timetable_group_id_index")
            .IsUnique();

        builder.Property(e => e.TimetableId)
            .UseIdentityAlwaysColumn()
            .HasColumnName("timetable_id");
        builder.Property(e => e.Created)
            .HasDefaultValueSql("now()")
            .HasColumnName("created");
        builder.Property(e => e.DayId)
            .HasColumnName("day_id");
        builder.Property(e => e.Ended)
            .HasColumnName("ended");
        builder.Property(e => e.GroupId)
            .HasColumnName("group_id");
        builder.Property(e => e.WeekTypeId)
            .HasColumnName("week_type_id");

        builder.HasOne(d => d.Day)
            .WithMany(p => p.Timetables)
            .HasForeignKey(d => d.DayId)
            .HasConstraintName("timetable_day_id_fk");

        builder.HasOne(d => d.Group)
            .WithOne(p => p.Timetable)
            .HasForeignKey<Timetable>(d => d.GroupId)
            .HasConstraintName("timetable_group_id_fk");

        builder.HasOne(d => d.WeekType)
            .WithMany(p => p.Timetables)
            .HasForeignKey(d => d.WeekTypeId)
            .HasConstraintName("timetable_week_type_id_fk");
    }
}