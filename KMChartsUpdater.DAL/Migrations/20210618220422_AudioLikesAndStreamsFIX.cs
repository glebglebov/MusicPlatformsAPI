using Microsoft.EntityFrameworkCore.Migrations;

namespace KMChartsUpdater.DAL.Migrations
{
    public partial class AudioLikesAndStreamsFIX : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Audios");

            migrationBuilder.DropColumn(
                name: "Streams",
                table: "Audios");

            migrationBuilder.AddColumn<long>(
                name: "Likes",
                table: "PositionFixes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Streams",
                table: "PositionFixes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Likes",
                table: "PositionFixes");

            migrationBuilder.DropColumn(
                name: "Streams",
                table: "PositionFixes");

            migrationBuilder.AddColumn<long>(
                name: "Likes",
                table: "Audios",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Streams",
                table: "Audios",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
