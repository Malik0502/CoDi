using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoDi.Data.Migrations
{
    /// <inheritdoc />
    public partial class changedSongIdColumName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Song",
                newName: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Song",
                newName: "Id");
        }
    }
}
