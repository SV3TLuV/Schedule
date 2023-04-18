using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class ClassroomClassroomTypeEntityTypeConfiguration
    : IEntityTypeConfiguration<ClassroomClassroomType>
{
    public void Configure(EntityTypeBuilder<ClassroomClassroomType> builder)
    {
        builder.ToTable("ClassroomClassroomTypes");
        builder.HasKey(e => new { e.ClassroomId, e.ClassroomTypeId })
            .HasName("PK_ClassroomClassroomTypes");
    }
}