using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class DisciplineCodeEntityTypeConfiguration : IEntityTypeConfiguration<DisciplineCode>
{
    public void Configure(EntityTypeBuilder<DisciplineCode> builder)
    {
        builder.HasKey(e => e.DisciplineCodeId)
            .HasName("discipline_code_pk");

        builder.ToTable("discipline_code");

        builder.HasIndex(e => e.Code, "discipline_code_code_index")
            .IsUnique();

        builder.Property(e => e.DisciplineCodeId)
            .UseIdentityAlwaysColumn()
            .HasColumnName("discipline_code_id");
        builder.Property(e => e.Code)
            .HasMaxLength(20)
            .HasColumnName("code");
        builder.Property(e => e.IsDeleted)
            .HasColumnName("is_deleted");
    }
}