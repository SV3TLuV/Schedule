using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Core.Models;

namespace Schedule.Persistence.Configurations;

public sealed class ClassroomEntityTypeConfiguration : IEntityTypeConfiguration<Classroom>
{
    public void Configure(EntityTypeBuilder<Classroom> builder)
    {
        builder.ToTable(tb => tb.HasTrigger("Classrooms_Delete"));
        builder.HasIndex(e => e.Cabinet, "IX_Classrooms").IsUnique();
        builder.Property(e => e.Cabinet).HasMaxLength(10);
        builder.HasData(new Classroom[]
        {
            new()
            {
                ClassroomId = 1,
                Cabinet = "0108",
            },
            new()
            {
                ClassroomId = 2,
                Cabinet = "0109",
            },
            new()
            {
                ClassroomId = 3,
                Cabinet = "0110",
            },
            new()
            {
                ClassroomId = 4,
                Cabinet = "0111",
            },
            new()
            {
                ClassroomId = 5,
                Cabinet = "0114",
            },
            new()
            {
                ClassroomId = 6,
                Cabinet = "0115",
            },
            new()
            {
                ClassroomId = 7,
                Cabinet = "0200",
            },
            new()
            {
                ClassroomId = 8,
                Cabinet = "0201",
            },
            new()
            {
                ClassroomId = 9,
                Cabinet = "0201а",
            },
            new()
            {
                ClassroomId = 10,
                Cabinet = "0202",
            },
            new()
            {
                ClassroomId = 11,
                Cabinet = "0204",
            },
            new()
            {
                ClassroomId = 12,
                Cabinet = "0205",
            },
            new()
            {
                ClassroomId = 13,
                Cabinet = "0207",
            },
            new()
            {
                ClassroomId = 14,
                Cabinet = "0209",
            },
            new()
            {
                ClassroomId = 15,
                Cabinet = "0209а",
            },
            new()
            {
                ClassroomId = 16,
                Cabinet = "0300",
            },
            new()
            {
                ClassroomId = 17,
                Cabinet = "0301",
            },
            new()
            {
                ClassroomId = 18,
                Cabinet = "0302",
            },
            new()
            {
                ClassroomId = 19,
                Cabinet = "0303",
            },
            new()
            {
                ClassroomId = 20,
                Cabinet = "0305",
            },
            new()
            {
                ClassroomId = 21,
                Cabinet = "0306",
            },
            new()
            {
                ClassroomId = 22,
                Cabinet = "0307",
            },
            new()
            {
                ClassroomId = 23,
                Cabinet = "0308",
            },
            new()
            {
                ClassroomId = 24,
                Cabinet = "0309",
            },
            new()
            {
                ClassroomId = 25,
                Cabinet = "104",
            },
            new()
            {
                ClassroomId = 26,
                Cabinet = "105",
            },
            new()
            {
                ClassroomId = 27,
                Cabinet = "215",
            },
            new()
            {
                ClassroomId = 28,
                Cabinet = "219",
            },
            new()
            {
                ClassroomId = 29,
                Cabinet = "220",
            },
            new()
            {
                ClassroomId = 30,
                Cabinet = "221",
            },
            new()
            {
                ClassroomId = 31,
                Cabinet = "222",
            },
            new()
            {
                ClassroomId = 32,
                Cabinet = "226",
            },
            new()
            {
                ClassroomId = 33,
                Cabinet = "228",
            },
            new()
            {
                ClassroomId = 34,
                Cabinet = "230",
            },
            new()
            {
                ClassroomId = 35,
                Cabinet = "300",
            },
            new()
            {
                ClassroomId = 36,
                Cabinet = "301",
            },
            new()
            {
                ClassroomId = 37,
                Cabinet = "303",
            },
            new()
            {
                ClassroomId = 38,
                Cabinet = "304",
            },
            new()
            {
                ClassroomId = 39,
                Cabinet = "305",
            },
            new()
            {
                ClassroomId = 40,
                Cabinet = "306",
            },
            new()
            {
                ClassroomId = 41,
                Cabinet = "306а",
            },
            new()
            {
                ClassroomId = 42,
                Cabinet = "307",
            },
            new()
            {
                ClassroomId = 43,
                Cabinet = "308",
            },
            new()
            {
                ClassroomId = 44,
                Cabinet = "309",
            },
            new()
            {
                ClassroomId = 45,
                Cabinet = "311",
            },
            new()
            {
                ClassroomId = 46,
                Cabinet = "312",
            },
            new()
            {
                ClassroomId = 47,
                Cabinet = "314",
            },
            new()
            {
                ClassroomId = 48,
                Cabinet = "315",
            },
            new()
            {
                ClassroomId = 49,
                Cabinet = "317",
            },
            new()
            {
                ClassroomId = 50,
                Cabinet = "401",
            },
            new()
            {
                ClassroomId = 51,
                Cabinet = "402",
            },
            new()
            {
                ClassroomId = 52,
                Cabinet = "403",
            },
            new()
            {
                ClassroomId = 53,
                Cabinet = "404",
            },
            new()
            {
                ClassroomId = 54,
                Cabinet = "404а",
            },
            new()
            {
                ClassroomId = 55,
                Cabinet = "405",
            },
            new()
            {
                ClassroomId = 56,
                Cabinet = "406",
            },
            new()
            {
                ClassroomId = 57,
                Cabinet = "407",
            },
            new()
            {
                ClassroomId = 58,
                Cabinet = "408",
            },
            new()
            {
                ClassroomId = 59,
                Cabinet = "409",
            },
            new()
            {
                ClassroomId = 60,
                Cabinet = "411",
            },
            new()
            {
                ClassroomId = 61,
                Cabinet = "411а",
            },
            new()
            {
                ClassroomId = 62,
                Cabinet = "413",
            },
            new()
            {
                ClassroomId = 63,
                Cabinet = "414",
            },
            new()
            {
                ClassroomId = 64,
                Cabinet = "416",
            },
            new()
            {
                ClassroomId = 65,
                Cabinet = "417",
            },
            new()
            {
                ClassroomId = 66,
                Cabinet = "418",
            },
        });
    }
}