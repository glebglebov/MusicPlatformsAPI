using Microsoft.EntityFrameworkCore.Migrations;

namespace KMChartsUpdater.DAL.Migrations
{
    public partial class normalized : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArtistNormalized",
                table: "Audios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleNormalized",
                table: "Audios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArtistNormalized",
                table: "Albums",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleNormalized",
                table: "Albums",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArtistNormalized",
                table: "Audios");

            migrationBuilder.DropColumn(
                name: "TitleNormalized",
                table: "Audios");

            migrationBuilder.DropColumn(
                name: "ArtistNormalized",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "TitleNormalized",
                table: "Albums");
        }
    }
}
