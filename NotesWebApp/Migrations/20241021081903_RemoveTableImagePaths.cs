using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotesWebApp.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTableImagePaths : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePaths",
                table: "Notes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePaths",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
