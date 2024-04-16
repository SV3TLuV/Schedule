using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class SessionEntityTypeConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.HasKey(e => e.SessionId)
            .HasName("session_pk");

        builder.ToTable("session");

        builder.Property(e => e.SessionId)
            .UseIdentityAlwaysColumn()
            .HasColumnName("session_id");
        builder.Property(e => e.AccountId)
            .HasColumnName("account_id");
        builder.Property(e => e.Created)
            .HasColumnName("created");
        builder.Property(e => e.RefreshToken)
            .HasMaxLength(512)
            .HasColumnName("refresh_token");
        builder.Property(e => e.Updated)
            .HasColumnName("updated");

        builder.HasOne(d => d.Account)
            .WithMany(p => p.Sessions)
            .HasForeignKey(d => d.AccountId)
            .HasConstraintName("session_account_id_fk");
    }
}