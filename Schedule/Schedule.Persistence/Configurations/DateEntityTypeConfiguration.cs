using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class DateEntityTypeConfiguration : IEntityTypeConfiguration<Date>
{
    public void Configure(EntityTypeBuilder<Date> builder)
    {
        builder.HasIndex(e => e.Value, "IX_Dates").IsUnique();
        builder.Property(e => e.Value).HasColumnType("date");
        builder.Property(e => e.TimeTypeId).HasDefaultValueSql();
        builder.HasOne(d => d.Day)
            .WithMany(p => p.Dates)
            .HasForeignKey(d => d.DayId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Dates_Days");
        builder.HasOne(d => d.TimeType)
            .WithMany(p => p.Dates)
            .HasForeignKey(d => d.TimeTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Dates_TimeTypes");
        builder.HasOne(d => d.WeekType)
            .WithMany(p => p.Dates)
            .HasForeignKey(d => d.WeekTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Dates_WeekTypes");
    }
}