using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Schedule.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classrooms",
                columns: table => new
                {
                    ClassroomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cabinet = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classrooms", x => x.ClassroomId);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    DayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsStudy = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.DayId);
                });

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

            migrationBuilder.CreateTable(
                name: "DisciplineType",
                columns: table => new
                {
                    DisciplineTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineType", x => x.DisciplineTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    TeacherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.TeacherId);
                });

            migrationBuilder.CreateTable(
                name: "TimeTypes",
                columns: table => new
                {
                    TimeTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeTypes", x => x.TimeTypeId);
                });

            migrationBuilder.CreateTable(
                name: "WeekTypes",
                columns: table => new
                {
                    WeekTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekTypes", x => x.WeekTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Terms",
                columns: table => new
                {
                    TermId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    CourseTerm = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terms", x => x.TermId);
                    table.ForeignKey(
                        name: "FK_Terms_Courses",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Roles",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId");
                });

            migrationBuilder.CreateTable(
                name: "Times",
                columns: table => new
                {
                    TimeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<TimeSpan>(type: "time", nullable: false),
                    End = table.Column<TimeSpan>(type: "time", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    LessonNumber = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Times", x => x.TimeId);
                    table.ForeignKey(
                        name: "FK_Times_TimeTypes",
                        column: x => x.TypeId,
                        principalTable: "TimeTypes",
                        principalColumn: "TimeTypeId");
                });

            migrationBuilder.CreateTable(
                name: "Dates",
                columns: table => new
                {
                    DateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<DateTime>(type: "date", nullable: false),
                    Term = table.Column<int>(type: "int", nullable: false),
                    DayId = table.Column<int>(type: "int", nullable: false),
                    WeekTypeId = table.Column<int>(type: "int", nullable: false),
                    IsStudy = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dates", x => x.DateId);
                    table.ForeignKey(
                        name: "FK_Dates_Days",
                        column: x => x.DayId,
                        principalTable: "Days",
                        principalColumn: "DayId");
                    table.ForeignKey(
                        name: "FK_Dates_WeekTypes",
                        column: x => x.WeekTypeId,
                        principalTable: "WeekTypes",
                        principalColumn: "WeekTypeId");
                });

            migrationBuilder.CreateTable(
                name: "Specialities",
                columns: table => new
                {
                    SpecialityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MaxTermId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialities", x => x.SpecialityId);
                    table.ForeignKey(
                        name: "FK_Specialities_Terms",
                        column: x => x.MaxTermId,
                        principalTable: "Terms",
                        principalColumn: "TermId");
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    SessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.SessionId);
                    table.ForeignKey(
                        name: "FK_Sessions_Users",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Disciplines",
                columns: table => new
                {
                    DisciplineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameId = table.Column<int>(type: "int", nullable: false),
                    CodeId = table.Column<int>(type: "int", nullable: false),
                    TotalHours = table.Column<int>(type: "int", nullable: false),
                    TermId = table.Column<int>(type: "int", nullable: false),
                    SpecialityId = table.Column<int>(type: "int", nullable: false),
                    DisciplineTypeId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplines", x => x.DisciplineId);
                    table.ForeignKey(
                        name: "FK_Disciplines_DisciplineCodes",
                        column: x => x.CodeId,
                        principalTable: "DisciplineCodes",
                        principalColumn: "DisciplineCodeId");
                    table.ForeignKey(
                        name: "FK_Disciplines_DisciplineNames",
                        column: x => x.NameId,
                        principalTable: "DisciplineNames",
                        principalColumn: "DisciplineNameId");
                    table.ForeignKey(
                        name: "FK_Disciplines_DisciplineType",
                        column: x => x.DisciplineTypeId,
                        principalTable: "DisciplineType",
                        principalColumn: "DisciplineTypeId");
                    table.ForeignKey(
                        name: "FK_Disciplines_Specialities",
                        column: x => x.SpecialityId,
                        principalTable: "Specialities",
                        principalColumn: "SpecialityId");
                    table.ForeignKey(
                        name: "FK_Disciplines_Terms",
                        column: x => x.TermId,
                        principalTable: "Terms",
                        principalColumn: "TermId");
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: false),
                    SpecialityId = table.Column<int>(type: "int", nullable: false),
                    TermId = table.Column<int>(type: "int", nullable: false),
                    EnrollmentYear = table.Column<int>(type: "int", nullable: false),
                    IsAfterEleven = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.GroupId);
                    table.ForeignKey(
                        name: "FK_Groups_Specialities",
                        column: x => x.SpecialityId,
                        principalTable: "Specialities",
                        principalColumn: "SpecialityId");
                    table.ForeignKey(
                        name: "FK_Groups_Terms",
                        column: x => x.TermId,
                        principalTable: "Terms",
                        principalColumn: "TermId");
                });

            migrationBuilder.CreateTable(
                name: "GroupGroups",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    GroupId2 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupGroups", x => new { x.GroupId, x.GroupId2 });
                    table.ForeignKey(
                        name: "FK_GroupGroups_Groups",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId");
                    table.ForeignKey(
                        name: "FK_GroupGroups_Groups1",
                        column: x => x.GroupId2,
                        principalTable: "Groups",
                        principalColumn: "GroupId");
                });

            migrationBuilder.CreateTable(
                name: "GroupTransfers",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    NextTermId = table.Column<int>(type: "int", nullable: false),
                    IsTransferred = table.Column<bool>(type: "bit", nullable: false),
                    TransferDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTransfers", x => new { x.GroupId, x.NextTermId });
                    table.ForeignKey(
                        name: "FK_GroupTransfers_Terms",
                        column: x => x.NextTermId,
                        principalTable: "Terms",
                        principalColumn: "TermId");
                    table.ForeignKey(
                        name: "FK_TransferingGroupsHistory_Groups",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId");
                });

            migrationBuilder.CreateTable(
                name: "Templates",
                columns: table => new
                {
                    TemplateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    TermId = table.Column<int>(type: "int", nullable: false),
                    DayId = table.Column<int>(type: "int", nullable: false),
                    WeekTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Templates", x => x.TemplateId);
                    table.ForeignKey(
                        name: "FK_Templates_Days",
                        column: x => x.DayId,
                        principalTable: "Days",
                        principalColumn: "DayId");
                    table.ForeignKey(
                        name: "FK_Templates_Groups",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId");
                    table.ForeignKey(
                        name: "FK_Templates_Terms",
                        column: x => x.TermId,
                        principalTable: "Terms",
                        principalColumn: "TermId");
                    table.ForeignKey(
                        name: "FK_Templates_WeekTypes",
                        column: x => x.WeekTypeId,
                        principalTable: "WeekTypes",
                        principalColumn: "WeekTypeId");
                });

            migrationBuilder.CreateTable(
                name: "Timetables",
                columns: table => new
                {
                    TimetableId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    DateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timetables", x => x.TimetableId);
                    table.ForeignKey(
                        name: "FK_Timetables_Dates",
                        column: x => x.DateId,
                        principalTable: "Dates",
                        principalColumn: "DateId");
                    table.ForeignKey(
                        name: "FK_Timetables_Groups",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId");
                });

            migrationBuilder.CreateTable(
                name: "LessonTemplates",
                columns: table => new
                {
                    LessonTemplateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Subgroup = table.Column<int>(type: "int", nullable: true),
                    TimeId = table.Column<int>(type: "int", nullable: true),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    DisciplineId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonTemplates", x => x.LessonTemplateId);
                    table.ForeignKey(
                        name: "FK_LessonTemplates_Disciplines",
                        column: x => x.DisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "DisciplineId");
                    table.ForeignKey(
                        name: "FK_LessonTemplates_Templates",
                        column: x => x.TemplateId,
                        principalTable: "Templates",
                        principalColumn: "TemplateId");
                    table.ForeignKey(
                        name: "FK_LessonTemplates_Times",
                        column: x => x.TimeId,
                        principalTable: "Times",
                        principalColumn: "TimeId");
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    LessonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Subgroup = table.Column<int>(type: "int", nullable: true),
                    TimeId = table.Column<int>(type: "int", nullable: true),
                    TimetableId = table.Column<int>(type: "int", nullable: false),
                    DisciplineId = table.Column<int>(type: "int", nullable: true),
                    IsChanged = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.LessonId);
                    table.ForeignKey(
                        name: "FK_Lessons_Disciplines",
                        column: x => x.DisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "DisciplineId");
                    table.ForeignKey(
                        name: "FK_Lessons_Times",
                        column: x => x.TimeId,
                        principalTable: "Times",
                        principalColumn: "TimeId");
                    table.ForeignKey(
                        name: "FK_Lessons_Timetables",
                        column: x => x.TimetableId,
                        principalTable: "Timetables",
                        principalColumn: "TimetableId");
                });

            migrationBuilder.CreateTable(
                name: "LessonTemplateTeacherClassrooms",
                columns: table => new
                {
                    LessonTemplateId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    ClassroomId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonTemplateTeacherClassrooms", x => new { x.LessonTemplateId, x.TeacherId });
                    table.ForeignKey(
                        name: "FK_LessonTemplateTeacherClassrooms_Classrooms",
                        column: x => x.ClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "ClassroomId");
                    table.ForeignKey(
                        name: "FK_LessonTemplateTeacherClassrooms_LessonTemplates",
                        column: x => x.LessonTemplateId,
                        principalTable: "LessonTemplates",
                        principalColumn: "LessonTemplateId");
                    table.ForeignKey(
                        name: "FK_LessonTemplateTeacherClassrooms_Teachers",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "TeacherId");
                });

            migrationBuilder.CreateTable(
                name: "LessonTeacherClassrooms",
                columns: table => new
                {
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    ClassroomId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonTeachers", x => new { x.LessonId, x.TeacherId });
                    table.ForeignKey(
                        name: "FK_LessonTeacherClassrooms_Classrooms",
                        column: x => x.ClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "ClassroomId");
                    table.ForeignKey(
                        name: "FK_LessonTeacherClassrooms_Lessons",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "LessonId");
                    table.ForeignKey(
                        name: "FK_LessonTeacherClassrooms_Teachers",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "TeacherId");
                });

            migrationBuilder.InsertData(
                table: "Classrooms",
                columns: new[] { "ClassroomId", "Cabinet", "IsDeleted" },
                values: new object[,]
                {
                    { 1, "0108", false },
                    { 2, "0109", false },
                    { 3, "0110", false },
                    { 4, "0111", false },
                    { 5, "0114", false },
                    { 6, "0115", false },
                    { 7, "0200", false },
                    { 8, "0201", false },
                    { 9, "0201а", false },
                    { 10, "0202", false },
                    { 11, "0204", false },
                    { 12, "0205", false },
                    { 13, "0207", false },
                    { 14, "0209", false },
                    { 15, "0209а", false },
                    { 16, "0300", false },
                    { 17, "0301", false },
                    { 18, "0302", false },
                    { 19, "0303", false },
                    { 20, "0305", false },
                    { 21, "0306", false },
                    { 22, "0307", false },
                    { 23, "0308", false },
                    { 24, "0309", false },
                    { 25, "104", false },
                    { 26, "105", false },
                    { 27, "215", false },
                    { 28, "219", false },
                    { 29, "220", false },
                    { 30, "221", false },
                    { 31, "222", false },
                    { 32, "226", false },
                    { 33, "228", false },
                    { 34, "230", false },
                    { 35, "300", false },
                    { 36, "301", false },
                    { 37, "303", false },
                    { 38, "304", false },
                    { 39, "305", false },
                    { 40, "306", false },
                    { 41, "306а", false },
                    { 42, "307", false },
                    { 43, "308", false },
                    { 44, "309", false },
                    { 45, "311", false },
                    { 46, "312", false },
                    { 47, "314", false },
                    { 48, "315", false },
                    { 49, "317", false },
                    { 50, "401", false },
                    { 51, "402", false },
                    { 52, "403", false },
                    { 53, "404", false },
                    { 54, "404а", false },
                    { 55, "405", false },
                    { 56, "406", false },
                    { 57, "407", false },
                    { 58, "408", false },
                    { 59, "409", false },
                    { 60, "411", false },
                    { 61, "411а", false },
                    { 62, "413", false },
                    { 63, "414", false },
                    { 64, "416", false },
                    { 65, "417", false },
                    { 66, "418", false }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                column: "CourseId",
                values: new object[]
                {
                    1,
                    2,
                    3,
                    4,
                    5
                });

            migrationBuilder.InsertData(
                table: "Days",
                columns: new[] { "DayId", "IsStudy", "Name" },
                values: new object[,]
                {
                    { 1, true, "Понедельник" },
                    { 2, true, "Вторник" },
                    { 3, true, "Среда" },
                    { 4, true, "Четверг" },
                    { 5, true, "Пятница" },
                    { 6, true, "Суббота" },
                    { 7, false, "Воскресенье" }
                });

            migrationBuilder.InsertData(
                table: "DisciplineCodes",
                columns: new[] { "DisciplineCodeId", "Code", "IsDeleted" },
                values: new object[,]
                {
                    { 1, "ЕН.01", false },
                    { 2, "ЕН.02", false },
                    { 3, "ЕН.03", false },
                    { 4, "ОГСЭ.01", false },
                    { 5, "ОГСЭ.02", false },
                    { 6, "ОГСЭ.03", false },
                    { 7, "ОГСЭ.04", false },
                    { 8, "ОГСЭ.05", false },
                    { 9, "ОП.01", false },
                    { 10, "ОП.02", false },
                    { 11, "ОП.03", false },
                    { 12, "ОП.04", false },
                    { 13, "ОП.05", false },
                    { 14, "ОП.06", false },
                    { 15, "ОП.07", false },
                    { 16, "ОП.08", false },
                    { 17, "ОП.09", false },
                    { 18, "ОП.10", false },
                    { 19, "ОП.11", false },
                    { 20, "ОП.12", false },
                    { 21, "ОП.13", false },
                    { 22, "ОП.14", false },
                    { 23, "ОУД.01", false },
                    { 24, "ОУД.02", false },
                    { 25, "ОУД.03", false },
                    { 26, "ОУД.04", false },
                    { 27, "ОУД.05", false },
                    { 28, "ОУД.06", false },
                    { 29, "ОУД.07", false },
                    { 30, "ОУД.08", false },
                    { 31, "ОУД.09", false },
                    { 32, "ОУД.10", false },
                    { 33, "ОУД.11", false },
                    { 34, "ОУД.12", false },
                    { 35, "ОУД.13", false },
                    { 36, "СГ.01", false },
                    { 37, "СГ.02", false },
                    { 38, "СГ.04", false },
                    { 39, "СГ.05", false },
                    { 40, "СГ.06", false },
                    { 41, "СГ.07", false }
                });

            migrationBuilder.InsertData(
                table: "DisciplineNames",
                columns: new[] { "DisciplineNameId", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, false, "ААС" },
                    { 2, false, "БЖД" },
                    { 3, false, "БИОЛОГИЯ" },
                    { 4, false, "ВТ" },
                    { 5, false, "ГЕОГРАФИЯ" },
                    { 6, false, "ДМ" },
                    { 7, false, "ДМ С ЭМЛ" },
                    { 8, false, "И И КГ" },
                    { 9, false, "ИБ" },
                    { 11, false, "ИКГ" },
                    { 12, false, "ИНДИВИДУАЛЬНЫЙ ПРОЕКТ" },
                    { 13, false, "ИНЖЕНЕРНАЯ ГРАФИКА" },
                    { 14, false, "ИНОСТРАННЫЙ ЯЗЫК" },
                    { 15, false, "ИНОСТРАННЫЙ ЯЗЫК В ПД" },
                    { 16, false, "ИНФОРМАТИКА" },
                    { 17, false, "ИП" },
                    { 18, false, "ИСТОРИЯ" },
                    { 19, false, "ИСТОРИЯ РОССИИ" },
                    { 20, false, "ИТ" },
                    { 21, false, "КМ" },
                    { 22, false, "КС" },
                    { 23, false, "ЛИТЕРАТУРА" },
                    { 24, false, "МАТЕМАТИКА" },
                    { 25, false, "МДК.01.01" },
                    { 26, false, "МДК.01.02" },
                    { 27, false, "МДК.01.03" },
                    { 28, false, "МДК.01.04" },
                    { 29, false, "МДК.01.05" },
                    { 30, false, "МДК.02.01" },
                    { 31, false, "МДК.02.02" },
                    { 32, false, "МДК.02.03" },
                    { 33, false, "МДК.03.01" },
                    { 34, false, "МДК.03.02" },
                    { 35, false, "МДК.03.03" },
                    { 36, false, "МДК.03.04" },
                    { 37, false, "МДК.03.05" },
                    { 38, false, "МДК.04.01" },
                    { 39, false, "МДК.04.02" },
                    { 40, false, "МДК.05.01" },
                    { 41, false, "МДК.05.02" },
                    { 42, false, "МДК.05.03" },
                    { 43, false, "МДК.06.01" },
                    { 44, false, "МДК.08.01" },
                    { 45, false, "МДК.08.02" },
                    { 46, false, "МДК.09.01" },
                    { 47, false, "МДК.09.02" },
                    { 48, false, "МДК.09.03" },
                    { 49, false, "МДК.11.01" },
                    { 50, false, "МЕНЕДЖМЕНТ" },
                    { 51, false, "МЕНЕДЖМЕНТ В ПД" },
                    { 53, false, "МС И С" },
                    { 54, false, "О И ПОИБ" },
                    { 55, false, "ОА И П" },
                    { 56, false, "ОБЖ" },
                    { 57, false, "ОБП" },
                    { 58, false, "ОБЩЕСТВОЗНАНИЕ" },
                    { 59, false, "ОИБ" },
                    { 60, false, "ОПБД" },
                    { 61, false, "ОС И С" },
                    { 62, false, "ОСНОВЫ БЕРЕЖЛИВОГО ПРОИЗВОДСТВА" },
                    { 63, false, "ОСНОВЫ ФИЛОСОФИИ" },
                    { 64, false, "ОСНОВЫ ФИНАНСОВОЙ ГРАМОТНОСТИ" },
                    { 65, false, "ОСНОВЫ ЭТ" },
                    { 66, false, "ОСНОВЫ ЭТХ" },
                    { 67, false, "ОТИ" },
                    { 68, false, "ОТК" },
                    { 69, false, "ОФГ" },
                    { 70, false, "ОЭ И ВТ" },
                    { 71, false, "ОЭТХ" },
                    { 73, false, "ПОКС И WEB-СЕРВЕРОВ" },
                    { 74, false, "ПОПД" },
                    { 75, false, "ПП" },
                    { 76, false, "ПП.01" },
                    { 77, false, "ПП.02" },
                    { 78, false, "ПП.03" },
                    { 79, false, "ПП.04" },
                    { 80, false, "ПП.05" },
                    { 81, false, "ПП.06" },
                    { 82, false, "ПП.08" },
                    { 83, false, "ПП.09" },
                    { 84, false, "ПП.11" },
                    { 85, false, "ППОПД" },
                    { 86, false, "ПРИКЛАДНАЯ ЭЛЕКТРОНИКА" },
                    { 87, false, "ПСИХОЛОГИЯ ОБЩЕНИЯ" },
                    { 88, false, "РУССКИЙ ЯЗЫК" },
                    { 89, false, "РЯ И КР" },
                    { 90, false, "СС И ТД" },
                    { 91, false, "ТВИМС" },
                    { 92, false, "ТФУПД" },
                    { 93, false, "ТЭС" },
                    { 94, false, "ТЭЦ" },
                    { 95, false, "УП.01" },
                    { 96, false, "УП.02" },
                    { 97, false, "УП.03" },
                    { 98, false, "УП.04" },
                    { 99, false, "УП.05" },
                    { 100, false, "УП.06" },
                    { 101, false, "УП.08" },
                    { 102, false, "УП.09" },
                    { 103, false, "УП.11" },
                    { 104, false, "ФИЗИКА" },
                    { 105, false, "ФИЗИЧЕСКАЯ КУЛЬТУРА" },
                    { 107, false, "ХИМИЯ" },
                    { 108, false, "ЧИСЛЕННЫЕ МЕТОДЫ" },
                    { 109, false, "Э И СХТ" },
                    { 110, false, "ЭВМ" },
                    { 111, false, "ЭКОНОМИКА И УПРАВЛЕНИЕ" },
                    { 112, false, "ЭКОНОМИКА ОТРАСЛИ" },
                    { 113, false, "ЭЛЕКТРОННАЯ ТЕХНИКА" },
                    { 114, false, "ЭЛЕКТРОТЕХНИКА" },
                    { 115, false, "ЭРИ" },
                    { 116, false, "ЭТ" },
                    { 117, false, "ЭТИ" },
                    { 118, false, "ЭТС" }
                });

            migrationBuilder.InsertData(
                table: "DisciplineType",
                columns: new[] { "DisciplineTypeId", "Name" },
                values: new object[,]
                {
                    { 1, "Дисциплина" },
                    { 2, "Практика" },
                    { 3, "Внекласная деятельность" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Editor" }
                });

            migrationBuilder.InsertData(
                table: "TimeTypes",
                columns: new[] { "TimeTypeId", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, false, "Стандартное" },
                    { 2, false, "Сокращенное" },
                    { 3, false, "Понедельник" }
                });

            migrationBuilder.InsertData(
                table: "WeekTypes",
                columns: new[] { "WeekTypeId", "Name" },
                values: new object[,]
                {
                    { 1, "Знаменатель" },
                    { 2, "Числитель" }
                });

            migrationBuilder.InsertData(
                table: "Terms",
                columns: new[] { "TermId", "CourseId", "CourseTerm" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 2, 1 },
                    { 4, 2, 2 },
                    { 5, 3, 1 },
                    { 6, 3, 2 },
                    { 7, 4, 1 },
                    { 8, 4, 2 },
                    { 9, 5, 1 },
                    { 10, 5, 2 }
                });

            migrationBuilder.InsertData(
                table: "Times",
                columns: new[] { "TimeId", "Duration", "End", "IsDeleted", "LessonNumber", "Start", "TypeId" },
                values: new object[,]
                {
                    { 1, 2, new TimeSpan(0, 10, 10, 0, 0), false, 1, new TimeSpan(0, 8, 30, 0, 0), 1 },
                    { 2, 2, new TimeSpan(0, 12, 0, 0, 0), false, 2, new TimeSpan(0, 10, 20, 0, 0), 1 },
                    { 3, 2, new TimeSpan(0, 14, 20, 0, 0), false, 3, new TimeSpan(0, 12, 40, 0, 0), 1 },
                    { 4, 2, new TimeSpan(0, 16, 10, 0, 0), false, 4, new TimeSpan(0, 14, 30, 0, 0), 1 },
                    { 5, 2, new TimeSpan(0, 18, 0, 0, 0), false, 5, new TimeSpan(0, 16, 20, 0, 0), 1 },
                    { 6, 2, new TimeSpan(0, 9, 45, 0, 0), false, 1, new TimeSpan(0, 8, 30, 0, 0), 2 },
                    { 7, 2, new TimeSpan(0, 11, 10, 0, 0), false, 2, new TimeSpan(0, 9, 55, 0, 0), 2 },
                    { 8, 2, new TimeSpan(0, 12, 55, 0, 0), false, 3, new TimeSpan(0, 11, 40, 0, 0), 2 },
                    { 9, 2, new TimeSpan(0, 14, 20, 0, 0), false, 4, new TimeSpan(0, 13, 5, 0, 0), 2 },
                    { 10, 2, new TimeSpan(0, 15, 45, 0, 0), false, 5, new TimeSpan(0, 14, 30, 0, 0), 2 },
                    { 11, 1, new TimeSpan(0, 9, 15, 0, 0), false, 0, new TimeSpan(0, 8, 30, 0, 0), 3 },
                    { 12, 2, new TimeSpan(0, 11, 0, 0, 0), false, 1, new TimeSpan(0, 9, 20, 0, 0), 3 },
                    { 13, 2, new TimeSpan(0, 12, 50, 0, 0), false, 2, new TimeSpan(0, 11, 10, 0, 0), 3 },
                    { 14, 2, new TimeSpan(0, 15, 10, 0, 0), false, 3, new TimeSpan(0, 13, 30, 0, 0), 3 },
                    { 15, 2, new TimeSpan(0, 17, 0, 0, 0), false, 4, new TimeSpan(0, 15, 20, 0, 0), 3 },
                    { 16, 2, new TimeSpan(0, 18, 50, 0, 0), false, 5, new TimeSpan(0, 17, 10, 0, 0), 3 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Login", "PasswordHash", "RoleId" },
                values: new object[,]
                {
                    { 1, "Admin", "$2a$11$/AKGJmbjT9.J/pdMmIk7S.VItgYYrknXhoPAUsTRIUqzIUXVw25zq", 1 },
                    { 2, "Editor", "$2a$11$qtS1HuNq4Q/9/gnERQJunu9U0wEYvtxbN2Z8senRvOLUF1gn/OV3i", 2 }
                });

            migrationBuilder.InsertData(
                table: "Specialities",
                columns: new[] { "SpecialityId", "Code", "IsDeleted", "MaxTermId", "Name" },
                values: new object[,]
                {
                    { 1, "09.02.05", false, 10, "ПКС" },
                    { 2, "10.02.18", false, 8, "Р" },
                    { 3, "09.02.01", false, 8, "КСК" },
                    { 4, " 09.02.03 ", false, 8, "ПКС" },
                    { 5, " 09.02.06", false, 8, "ССА" },
                    { 6, "09.02.07 ", false, 8, "ИСПП" },
                    { 7, "09.02.07 ", false, 8, "ИСПВ" },
                    { 8, "10.02.04", false, 8, "ОИБ" },
                    { 9, "11.02.15", false, 8, "ИСС" },
                    { 10, "11.02.18", false, 8, "СР, МС И Т" },
                    { 11, "11.02.18", false, 8, "РМТ" },
                    { 12, "11.02.10", false, 8, "Р" },
                    { 13, "11.02.11", false, 8, "С" },
                    { 14, "09.02.03", false, 8, "ПКС" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms",
                table: "Classrooms",
                column: "Cabinet",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dates",
                table: "Dates",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dates_DayId",
                table: "Dates",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_Dates_WeekTypeId",
                table: "Dates",
                column: "WeekTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Days",
                table: "Days",
                column: "Name",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines",
                table: "Disciplines",
                columns: new[] { "CodeId", "NameId", "SpecialityId", "TermId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_DisciplineTypeId",
                table: "Disciplines",
                column: "DisciplineTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_NameId",
                table: "Disciplines",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_SpecialityId",
                table: "Disciplines",
                column: "SpecialityId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_TermId",
                table: "Disciplines",
                column: "TermId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupGroups_GroupId2",
                table: "GroupGroups",
                column: "GroupId2");

            migrationBuilder.CreateIndex(
                name: "IX_Groups",
                table: "Groups",
                columns: new[] { "Number", "EnrollmentYear", "SpecialityId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_SpecialityId",
                table: "Groups",
                column: "SpecialityId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_TermId",
                table: "Groups",
                column: "TermId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTransfers_NextTermId",
                table: "GroupTransfers",
                column: "NextTermId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_DisciplineId",
                table: "Lessons",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_TimeId",
                table: "Lessons",
                column: "TimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_TimetableId",
                table: "Lessons",
                column: "TimetableId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonTeacherClassrooms_ClassroomId",
                table: "LessonTeacherClassrooms",
                column: "ClassroomId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonTeacherClassrooms_TeacherId",
                table: "LessonTeacherClassrooms",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonTemplates_DisciplineId",
                table: "LessonTemplates",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonTemplates_TemplateId",
                table: "LessonTemplates",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonTemplates_TimeId",
                table: "LessonTemplates",
                column: "TimeId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonTemplateTeacherClassrooms_ClassroomId",
                table: "LessonTemplateTeacherClassrooms",
                column: "ClassroomId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonTemplateTeacherClassrooms_TeacherId",
                table: "LessonTemplateTeacherClassrooms",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_UserId",
                table: "Sessions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Specialities",
                table: "Specialities",
                columns: new[] { "Code", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Specialities_MaxTermId",
                table: "Specialities",
                column: "MaxTermId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers",
                table: "Teachers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Templates_1",
                table: "Templates",
                columns: new[] { "GroupId", "TermId", "DayId", "WeekTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Templates_DayId",
                table: "Templates",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_GroupId",
                table: "Templates",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_TermId",
                table: "Templates",
                column: "TermId");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_WeekTypeId",
                table: "Templates",
                column: "WeekTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Terms",
                table: "Terms",
                columns: new[] { "CourseId", "CourseTerm" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Times",
                table: "Times",
                columns: new[] { "TypeId", "LessonNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Timetables",
                table: "Timetables",
                columns: new[] { "DateId", "GroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Timetables_DateId",
                table: "Timetables",
                column: "DateId");

            migrationBuilder.CreateIndex(
                name: "IX_Timetables_GroupId",
                table: "Timetables",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeTypes",
                table: "TimeTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users",
                table: "Users",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_WeekTypes",
                table: "WeekTypes",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupGroups");

            migrationBuilder.DropTable(
                name: "GroupTransfers");

            migrationBuilder.DropTable(
                name: "LessonTeacherClassrooms");

            migrationBuilder.DropTable(
                name: "LessonTemplateTeacherClassrooms");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "Classrooms");

            migrationBuilder.DropTable(
                name: "LessonTemplates");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Timetables");

            migrationBuilder.DropTable(
                name: "Disciplines");

            migrationBuilder.DropTable(
                name: "Templates");

            migrationBuilder.DropTable(
                name: "Times");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Dates");

            migrationBuilder.DropTable(
                name: "DisciplineCodes");

            migrationBuilder.DropTable(
                name: "DisciplineNames");

            migrationBuilder.DropTable(
                name: "DisciplineType");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "TimeTypes");

            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropTable(
                name: "WeekTypes");

            migrationBuilder.DropTable(
                name: "Specialities");

            migrationBuilder.DropTable(
                name: "Terms");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
