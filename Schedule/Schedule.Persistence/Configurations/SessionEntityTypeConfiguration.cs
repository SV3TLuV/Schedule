﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class SessionEntityTypeConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.HasKey(e => e.SessionId);
        builder.Property(e => e.RefreshToken).HasMaxLength(512);
        builder.HasOne(d => d.User)
            .WithMany(e => e.Sessions)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .HasConstraintName("FK_Sessions_Users");
    }
}