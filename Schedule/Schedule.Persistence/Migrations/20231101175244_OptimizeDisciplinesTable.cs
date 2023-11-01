using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schedule.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class OptimizeDisciplinesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pairs_Disciplines",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Pairs_Times",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Pairs_Timetables",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonTeacherClassrooms_Classrooms2",
                table: "LessonTeacherClassrooms");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonTeacherClassrooms_Teachers1",
                table: "LessonTeacherClassrooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PairTeachers",
                table: "LessonTeacherClassrooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pairs",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Disciplines",
                table: "Disciplines");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Disciplines");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Disciplines");

            migrationBuilder.AddColumn<int>(
                name: "CodeId",
                table: "Disciplines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NameId",
                table: "Disciplines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LessonTeachers",
                table: "LessonTeacherClassrooms",
                columns: new[] { "LessonId", "TeacherId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons",
                column: "LessonId");

            migrationBuilder.CreateTable(
                name: "DisciplineCodes",
                columns: table => new
                {
                    DisciplineCodeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineCodes", x => x.DisciplineCodeId);
                });

            migrationBuilder.CreateTable(
                name: "DisciplineNames",
                columns: table => new
                {
                    DisciplineNameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineNames", x => x.DisciplineNameId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines",
                table: "Disciplines",
                columns: new[] { "CodeId", "NameId", "SpecialityId", "TermId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_NameId",
                table: "Disciplines",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineCodes",
                table: "DisciplineCodes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineNames",
                table: "DisciplineNames",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Disciplines_DisciplineCodes",
                table: "Disciplines",
                column: "CodeId",
                principalTable: "DisciplineCodes",
                principalColumn: "DisciplineCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Disciplines_DisciplineNames",
                table: "Disciplines",
                column: "NameId",
                principalTable: "DisciplineNames",
                principalColumn: "DisciplineNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Disciplines",
                table: "Lessons",
                column: "DisciplineId",
                principalTable: "Disciplines",
                principalColumn: "DisciplineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Times",
                table: "Lessons",
                column: "TimeId",
                principalTable: "Times",
                principalColumn: "TimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Timetables",
                table: "Lessons",
                column: "TimetableId",
                principalTable: "Timetables",
                principalColumn: "TimetableId");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonTeacherClassrooms_Classrooms",
                table: "LessonTeacherClassrooms",
                column: "ClassroomId",
                principalTable: "Classrooms",
                principalColumn: "ClassroomId");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonTeacherClassrooms_Teachers",
                table: "LessonTeacherClassrooms",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "TeacherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Disciplines_DisciplineCodes",
                table: "Disciplines");

            migrationBuilder.DropForeignKey(
                name: "FK_Disciplines_DisciplineNames",
                table: "Disciplines");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Disciplines",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Times",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Timetables",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonTeacherClassrooms_Classrooms",
                table: "LessonTeacherClassrooms");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonTeacherClassrooms_Teachers",
                table: "LessonTeacherClassrooms");

            migrationBuilder.DropTable(
                name: "DisciplineCodes");

            migrationBuilder.DropTable(
                name: "DisciplineNames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LessonTeachers",
                table: "LessonTeacherClassrooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Disciplines",
                table: "Disciplines");

            migrationBuilder.DropIndex(
                name: "IX_Disciplines_NameId",
                table: "Disciplines");

            migrationBuilder.DropColumn(
                name: "CodeId",
                table: "Disciplines");

            migrationBuilder.DropColumn(
                name: "NameId",
                table: "Disciplines");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Disciplines",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Disciplines",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PairTeachers",
                table: "LessonTeacherClassrooms",
                columns: new[] { "LessonId", "TeacherId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pairs",
                table: "Lessons",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines",
                table: "Disciplines",
                columns: new[] { "Code", "Name", "SpecialityId", "TermId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pairs_Disciplines",
                table: "Lessons",
                column: "DisciplineId",
                principalTable: "Disciplines",
                principalColumn: "DisciplineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pairs_Times",
                table: "Lessons",
                column: "TimeId",
                principalTable: "Times",
                principalColumn: "TimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pairs_Timetables",
                table: "Lessons",
                column: "TimetableId",
                principalTable: "Timetables",
                principalColumn: "TimetableId");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonTeacherClassrooms_Classrooms2",
                table: "LessonTeacherClassrooms",
                column: "ClassroomId",
                principalTable: "Classrooms",
                principalColumn: "ClassroomId");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonTeacherClassrooms_Teachers1",
                table: "LessonTeacherClassrooms",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "TeacherId");
        }
    }
}
