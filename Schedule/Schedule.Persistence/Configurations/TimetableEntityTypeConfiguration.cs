﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class TimetableEntityTypeConfiguration : IEntityTypeConfiguration<Timetable>
{
    public void Configure(EntityTypeBuilder<Timetable> builder)
    {
        builder.ToTable(tb => tb.HasTrigger("Timetables_Insert"));
        builder.HasIndex(e => new { e.DateId, e.GroupId }, "IX_Timetables").IsUnique();
        builder.HasOne(d => d.Date).WithMany(p => p.Timetables)
            .HasForeignKey(d => d.DateId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Timetables_Dates");
        builder.HasOne(d => d.Group).WithMany(p => p.Timetables)
            .HasForeignKey(d => d.GroupId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Timetables_Groups");
    }
}