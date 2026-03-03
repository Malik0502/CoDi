using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoDi.Data.Migrations
{
    /// <inheritdoc />
    public partial class changedDailySongStats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "last_played_at",
                table: "daily_song_stats");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "time_played_sec",
                table: "daily_song_stats",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "time_played_sec",
                table: "daily_song_stats");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "last_played_at",
                table: "daily_song_stats",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }
    }
}
