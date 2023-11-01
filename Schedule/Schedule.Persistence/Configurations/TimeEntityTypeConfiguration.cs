using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class TimeEntityTypeConfiguration : IEntityTypeConfiguration<Time>
{
    public void Configure(EntityTypeBuilder<Time> builder)
    {
        builder.HasIndex(e => new { e.TypeId, e.LessonNumber }, "IX_Times").IsUnique();
        builder.HasOne(d => d.Type)
            .WithMany(p => p.Times)
            .HasForeignKey(d => d.TypeId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .HasConstraintName("FK_Times_TimeTypes");
        builder.HasData(new Time[]
        {
            new()
            {
                TimeId = 1,
                Start = new TimeSpan(8, 30, 0),
                End = new TimeSpan(10, 10, 0),
                Duration = 2,
                LessonNumber = 1,
                TypeId = 1,
            },
            new()
            {
                TimeId = 2,
                Start = new TimeSpan(10, 20, 0),
                End = new TimeSpan(12, 0, 0),
                Duration = 2,
                LessonNumber = 2,
                TypeId = 1,
            },
            new()
            {
                TimeId = 3,
                Start = new TimeSpan(12, 40, 0),
                End = new TimeSpan(14, 20, 0),
                Duration = 2,
                LessonNumber = 3,
                TypeId = 1,
            },
            new()
            {
                TimeId = 4,
                Start = new TimeSpan(14, 30, 0),
                End = new TimeSpan(16, 10, 0),
                Duration = 2,
                LessonNumber = 4,
                TypeId = 1,
            },
            new()
            {
                TimeId = 5,
                Start = new TimeSpan(16, 20, 0),
                End = new TimeSpan(18, 0, 0),
                Duration = 2,
                LessonNumber = 5,
                TypeId = 1,
            },
            new()
            {
                TimeId = 6,
                Start = new TimeSpan(8, 30, 0),
                End = new TimeSpan(9, 45, 0),
                Duration = 2,
                LessonNumber = 1,
                TypeId = 2,
            },
            new()
            {
                TimeId = 7,
                Start = new TimeSpan(9, 55, 0),
                End = new TimeSpan(11, 10, 0),
                Duration = 2,
                LessonNumber = 2,
                TypeId = 2,
            },
            new()
            {
                TimeId = 8,
                Start = new TimeSpan(11, 40, 0),
                End = new TimeSpan(12, 55, 0),
                Duration = 2,
                LessonNumber = 3,
                TypeId = 2,
            },
            new()
            {
                TimeId = 9,
                Start = new TimeSpan(13, 5, 0),
                End = new TimeSpan(14, 20, 0),
                Duration = 2,
                LessonNumber = 4,
                TypeId = 2,
            },
            new()
            {
                TimeId = 10,
                Start = new TimeSpan(14, 30, 0),
                End = new TimeSpan(15, 45, 0),
                Duration = 2,
                LessonNumber = 5,
                TypeId = 2,
            },
            new()
            {
                TimeId = 11,
                Start = new TimeSpan(8, 30, 0),
                End = new TimeSpan(9, 15, 0),
                Duration = 1,
                LessonNumber = 0,
                TypeId = 3,
            },
            new()
            {
                TimeId = 12,
                Start = new TimeSpan(9, 20, 0),
                End = new TimeSpan(11, 0, 0),
                Duration = 2,
                LessonNumber = 1,
                TypeId = 3,
            },
            new()
            {
                TimeId = 13,
                Start = new TimeSpan(11, 10, 0),
                End = new TimeSpan(12, 50, 0),
                Duration = 2,
                LessonNumber = 2,
                TypeId = 3,
            },
            new()
            {
                TimeId = 14,
                Start = new TimeSpan(13, 30, 0),
                End = new TimeSpan(15, 10, 0),
                Duration = 2,
                LessonNumber = 3,
                TypeId = 3,
            },
            new()
            {
                TimeId = 15,
                Start = new TimeSpan(15, 20, 0),
                End = new TimeSpan(17, 0, 0),
                Duration = 2,
                LessonNumber = 4,
                TypeId = 3,
            },
            new()
            {
                TimeId = 16,
                Start = new TimeSpan(17, 10, 0),
                End = new TimeSpan(18, 50, 0),
                Duration = 2,
                LessonNumber = 5,
                TypeId = 3,
            },
        });
    }
}