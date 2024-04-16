using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class GroupTransferEntityTypeConfiguration : IEntityTypeConfiguration<GroupTransfer>
{
    public void Configure(EntityTypeBuilder<GroupTransfer> builder)
    {
        builder.HasKey(e => new { e.NextTermId, e.GroupId })
            .HasName("group_transfer_pk");

        builder.ToTable("group_transfer");

        builder.Property(e => e.NextTermId)
            .HasColumnName("next_term_id");
        builder.Property(e => e.GroupId)
            .HasColumnName("group_id");
        builder.Property(e => e.IsTransferred)
            .HasColumnName("is_transferred");
        builder.Property(e => e.TransferDate)
            .HasColumnName("transfer_date");

        builder.HasOne(d => d.Group)
            .WithMany(p => p.GroupTransfers)
            .HasForeignKey(d => d.GroupId)
            .HasConstraintName("group_transfer_group_id_fk");

        builder.HasOne(d => d.NextTerm)
            .WithMany(p => p.GroupTransfers)
            .HasForeignKey(d => d.NextTermId)
            .HasConstraintName("group_transfer_next_term_id_fk");
    }
}