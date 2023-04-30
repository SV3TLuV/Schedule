using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class TransferingGroupsHistoryEntityTypeConfiguration
    : IEntityTypeConfiguration<TransferingGroupsHistory>
{
    public void Configure(EntityTypeBuilder<TransferingGroupsHistory> builder)
    {
        builder.HasKey(e => new { e.GroupId, e.Year });
        builder.ToTable("TransferingGroupsHistory");
        builder.Property(e => e.TransferDate).HasColumnType("date");
        builder.HasOne(d => d.Group).WithMany(p => p.TransferingGroupsHistories)
            .HasForeignKey(d => d.GroupId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_TransferingGroupsHistory_Groups");
    }
}