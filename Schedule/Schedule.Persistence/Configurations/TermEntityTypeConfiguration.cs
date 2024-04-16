using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class TermEntityTypeConfiguration : IEntityTypeConfiguration<Term>
{
    public void Configure(EntityTypeBuilder<Term> builder)
    {
        builder.HasKey(e => e.TermId)
            .HasName("term_pk");

        builder.ToTable("term");

        builder.Property(e => e.TermId)
            .ValueGeneratedNever()
            .HasColumnName("term_id");
        builder.Property(e => e.Course)
            .HasColumnName("course");
    }
}