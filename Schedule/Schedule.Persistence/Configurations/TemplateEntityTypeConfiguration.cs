using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class TemplateEntityTypeConfiguration : IEntityTypeConfiguration<Template>
{
    public void Configure(EntityTypeBuilder<Template> builder)
    {
        builder.HasIndex(e => new { e.GroupId, e.TermId, e.DayId, e.WeekTypeId }, "IX_Templates_1").IsUnique();
        builder.HasIndex(e => e.GroupId, "IX_Templates_GroupId");
        builder.HasOne(d => d.Day).WithMany(p => p.Templates)
            .HasForeignKey(d => d.DayId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Templates_Days");
        builder.HasOne(d => d.Group).WithMany(p => p.Templates)
            .HasForeignKey(d => d.GroupId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Templates_Groups");
        builder.HasOne(d => d.Term).WithMany(p => p.Templates)
            .HasForeignKey(d => d.TermId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Templates_Terms");
        builder.HasOne(d => d.WeekType).WithMany(p => p.Templates)
            .HasForeignKey(d => d.WeekTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Templates_WeekTypes");
    }
}