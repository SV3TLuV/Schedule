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
        builder.HasData(new Term[]
        {
            new()
            {
                TermId = 1,
                CourseId = 1,
                CourseTerm = 1,
            },
            new()
            {
                TermId = 2,
                CourseId = 1,
                CourseTerm = 2,
            },
            new()
            {
                TermId = 3,
                CourseId = 2,
                CourseTerm = 1,
            },
            new()
            {
                TermId = 4,
                CourseId = 2,
                CourseTerm = 2,
            },
            new()
            {
                TermId = 5,
                CourseId = 3,
                CourseTerm = 1,
            },
            new()
            {
                TermId = 6,
                CourseId = 3,
                CourseTerm = 2,
            },
            new()
            {
                TermId = 7,
                CourseId = 4,
                CourseTerm = 1,
            },
            new()
            {
                TermId = 8,
                CourseId = 4,
                CourseTerm = 2,
            },
            new()
            {
                TermId = 9,
                CourseId = 5,
                CourseTerm = 1,
            },
            new()
            {
                TermId = 10,
                CourseId = 5,
                CourseTerm = 2,
            },
        });
    }
}