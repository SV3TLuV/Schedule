using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schedule.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixDisciplinesUniqueConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Disciplines",
                table: "Disciplines");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines",
                table: "Disciplines",
                columns: new[] { "Code", "Name", "SpecialityId", "TermId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Disciplines",
                table: "Disciplines");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines",
                table: "Disciplines",
                columns: new[] { "Code", "Name", "SpecialityId" },
                unique: true);
        }
    }
}
