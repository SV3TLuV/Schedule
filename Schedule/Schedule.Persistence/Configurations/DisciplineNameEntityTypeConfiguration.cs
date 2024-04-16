using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class DisciplineNameEntityTypeConfiguration : IEntityTypeConfiguration<DisciplineName>
{
    public void Configure(EntityTypeBuilder<DisciplineName> builder)
    {
        builder.HasKey(e => e.DisciplineNameId)
            .HasName("discipline_name_pk");

        builder.ToTable("discipline_name");

        builder.HasIndex(e => e.Name, "discipline_name_name_index")
            .IsUnique();

        builder.Property(e => e.DisciplineNameId)
            .UseIdentityAlwaysColumn()
            .HasColumnName("discipline_name_id");
        builder.Property(e => e.Name)
            .HasMaxLength(50)
            .HasColumnName("name");
    }
}