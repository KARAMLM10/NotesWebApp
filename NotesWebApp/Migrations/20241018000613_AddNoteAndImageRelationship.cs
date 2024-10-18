using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotesWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddNoteAndImageRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "ImagePaths",
                table: "Notes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageData",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagePaths",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
