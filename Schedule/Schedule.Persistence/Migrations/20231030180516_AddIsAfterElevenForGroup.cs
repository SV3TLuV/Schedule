using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schedule.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddIsAfterElevenForGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAfterEleven",
                table: "Groups",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Times",
                keyColumn: "TimeId",
                keyValue: 6,
                column: "TypeId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Times",
                keyColumn: "TimeId",
                keyValue: 7,
                column: "TypeId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Times",
                keyColumn: "TimeId",
                keyValue: 8,
                column: "TypeId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Times",
                keyColumn: "TimeId",
                keyValue: 9,
                column: "TypeId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Times",
                keyColumn: "TimeId",
                keyValue: 10,
                column: "TypeId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Times",
                keyColumn: "TimeId",
                keyValue: 11,
                column: "TypeId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Times",
                keyColumn: "TimeId",
                keyValue: 12,
                column: "TypeId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Times",
                keyColumn: "TimeId",
                keyValue: 13,
                column: "TypeId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Times",
                keyColumn: "TimeId",
                keyValue: 14,
                column: "TypeId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Times",
                keyColumn: "TimeId",
                keyValue: 15,
                column: "TypeId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Times",
                keyColumn: "TimeId",
                keyValue: 16,
                column: "TypeId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "RoleId",
                value: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAfterEleven",
                table: "Groups");

            migrationBuilder.UpdateData(
                table: "Times",
                keyColumn: "TimeId",
                keyValue: 6,
                column: "TypeId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Times",
                keyColumn: "TimeId",
                keyValue: 7,
                column: "TypeId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Times",
                keyColumn: "TimeId",
                keyValue: 8,
                column: "TypeId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Times",
                keyColumn: "TimeId",
                keyValue: 9,
                column: "TypeId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Times",
                keyColumn: "TimeId",
                keyValue: 10,
                column: "TypeId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Times",
                keyColumn: "TimeId",
                keyValue: 11,
                column: "TypeId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Times",
                keyColumn: "TimeId",
                keyValue: 12,
                column: "TypeId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Times",
                keyColumn: "TimeId",
                keyValue: 13,
                column: "TypeId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Times",
                keyColumn: "TimeId",
                keyValue: 14,
                column: "TypeId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Times",
                keyColumn: "TimeId",
                keyValue: 15,
                column: "TypeId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Times",
                keyColumn: "TimeId",
                keyValue: 16,
                column: "TypeId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "RoleId",
                value: 1);
        }
    }
}
