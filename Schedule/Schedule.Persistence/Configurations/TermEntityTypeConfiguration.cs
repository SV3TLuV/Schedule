using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class TermEntityTypeConfiguration : IEntityTypeConfiguration<Term>
{
    public void Configure(EntityTypeBuilder<Term> builder)
    {
        builder.HasIndex(e => new { e.CourseId, e.CourseTerm }, "IX_Terms").IsUnique();
        builder.Property(e => e.TermId).ValueGeneratedNever();
        builder.HasOne(d => d.Course).WithMany(p => p.Terms)
            .HasForeignKey(d => d.CourseId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Terms_Courses");
    }
}