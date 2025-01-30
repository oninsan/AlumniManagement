using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlumniManagement.Migrations
{
    /// <inheritdoc />
    public partial class announcementImagePath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AnnouncementImagePath",
                table: "announcement",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnnouncementImagePath",
                table: "announcement");
        }
    }
}
