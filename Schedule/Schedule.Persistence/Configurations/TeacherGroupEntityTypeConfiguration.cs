using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class TeacherGroupEntityTypeConfiguration
    : IEntityTypeConfiguration<TeacherGroup>
{
    public void Configure(EntityTypeBuilder<TeacherGroup> builder)
    {
        builder.HasKey(e => new { e.TeacherId, e.GroupId })
            .HasName("PK_TeacherGroups");
    }
}