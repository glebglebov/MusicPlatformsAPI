using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KMChartsUpdater.DAL.Migrations
{
    public partial class playlistDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Playlists",
                table: "Reports");

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Playlists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Playlists");

            migrationBuilder.AddColumn<string>(
                name: "Playlists",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
