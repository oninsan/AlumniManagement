using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlumniManagement.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "alumni",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "user"),
                    password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    role = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    course_graduated = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    department = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    year_graduated = table.Column<int>(type: "int", nullable: false),
                    working_status = table.Column<bool>(type: "bit", nullable: false),
                    current_work = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    mobile_number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    picture = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    confirmation_status = table.Column<int>(type: "int", nullable: false, defaultValueSql: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alumni", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "announcement",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    announcement_title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    announcement_description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    date_created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcement", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "announcement_comment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    announcement_id = table.Column<int>(type: "int", nullable: false),
                    comment = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementComment", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "event",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    event_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    event_description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    EventDate = table.Column<DateTime>(type: "date", nullable: false),
                    event_time = table.Column<TimeSpan>(type: "time", nullable: false),
                    attendees_list = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    EventImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "event_comment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    event_id = table.Column<int>(type: "int", nullable: false),
                    comment = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventComment", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "event_like",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    event_id = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLike", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "alumni");

            migrationBuilder.DropTable(
                name: "announcement");

            migrationBuilder.DropTable(
                name: "announcement_comment");

            migrationBuilder.DropTable(
                name: "event");

            migrationBuilder.DropTable(
                name: "event_comment");

            migrationBuilder.DropTable(
                name: "event_like");
        }
    }
}
