using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class TimeEntityTypeConfiguration : IEntityTypeConfiguration<Time>
{
    public void Configure(EntityTypeBuilder<Time> builder)
    {
        builder.ToTable(tb => tb.HasTrigger("Times_Delete"));
        builder.HasIndex(e => new { e.TypeId, e.LessonNumber }, "IX_Times").IsUnique();
        builder.HasOne(d => d.Type)
            .WithMany(p => p.Times)
            .HasForeignKey(d => d.TypeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Times_TimeTypes");
    }
}