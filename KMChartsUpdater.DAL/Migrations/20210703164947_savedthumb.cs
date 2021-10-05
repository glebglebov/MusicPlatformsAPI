using Microsoft.EntityFrameworkCore.Migrations;

namespace KMChartsUpdater.DAL.Migrations
{
    public partial class savedthumb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SavedThumb",
                table: "Audios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SavedThumb",
                table: "Albums",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SavedThumb",
                table: "Audios");

            migrationBuilder.DropColumn(
                name: "SavedThumb",
                table: "Albums");
        }
    }
}
