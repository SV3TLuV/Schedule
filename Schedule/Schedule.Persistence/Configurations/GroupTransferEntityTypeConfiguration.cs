using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class GroupTransferEntityTypeConfiguration
    : IEntityTypeConfiguration<GroupTransfer>
{
    public void Configure(EntityTypeBuilder<GroupTransfer> builder)
    {
        builder.HasKey(e => new { e.GroupId, e.NextTermId });
        builder.Property(e => e.TransferDate).HasColumnType("date");
        builder.HasOne(d => d.Group)
            .WithMany(p => p.GroupTransfers)
            .HasForeignKey(d => d.GroupId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .HasConstraintName("FK_TransferingGroupsHistory_Groups");
        builder.HasOne(d => d.NextTerm)
            .WithMany(p => p.GroupTransfers)
            .HasForeignKey(d => d.NextTermId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_GroupTransfers_Terms");
    }
}