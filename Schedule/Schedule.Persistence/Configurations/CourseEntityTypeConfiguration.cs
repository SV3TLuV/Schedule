using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class CourseEntityTypeConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.Property(e => e.CourseId).ValueGeneratedNever();
        builder.HasData(new Course[]
        {
            new()
            {
                CourseId = 1,
            },
            new()
            {
                CourseId = 2,
            },
            new()
            {
                CourseId = 3,
            },
            new()
            {
                CourseId = 4,
            },
            new()
            {
                CourseId = 5,
            },
        });
    }
}