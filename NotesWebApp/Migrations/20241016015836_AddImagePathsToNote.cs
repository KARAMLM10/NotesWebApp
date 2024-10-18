using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotesWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddImagePathsToNote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Notes");

            migrationBuilder.AddColumn<string>(
                name: "ImagePaths",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePaths",
                table: "Notes");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Notes",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
