using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoDi.Data.Migrations
{
    /// <inheritdoc />
    public partial class changedSongColumnNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Song",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Artist",
                table: "Song",
                newName: "artist");

            migrationBuilder.RenameColumn(
                name: "DatePlayed",
                table: "Song",
                newName: "played_at");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Song",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "artist",
                table: "Song",
                newName: "Artist");

            migrationBuilder.RenameColumn(
                name: "played_at",
                table: "Song",
                newName: "DatePlayed");
        }
    }
}
