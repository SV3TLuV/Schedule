using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class GroupGroupEntityTypeConfiguration
    : IEntityTypeConfiguration<GroupGroup>
{
    public void Configure(EntityTypeBuilder<GroupGroup> builder)
    {
        builder.ToTable("GroupGroups");
        builder.HasKey(e => new { e.GroupId, e.GroupId2 })
            .HasName("PK_GroupGroups");
    }
}