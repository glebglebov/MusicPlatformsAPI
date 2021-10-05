using Microsoft.EntityFrameworkCore.Migrations;

namespace KMChartsUpdater.DAL.Migrations
{
    public partial class Playlists2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LabelToAlbums");

            migrationBuilder.DropTable(
                name: "PlaylistPositionFix");

            migrationBuilder.AddColumn<string>(
                name: "Playlists",
                table: "PositionFixes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdOnPlatform",
                table: "Playlists",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tracks",
                table: "Playlists",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Playlists",
                table: "PositionFixes");

            migrationBuilder.DropColumn(
                name: "IdOnPlatform",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "Tracks",
                table: "Playlists");

            migrationBuilder.CreateTable(
                name: "LabelToAlbums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlbumId = table.Column<int>(type: "int", nullable: true),
                    LabelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabelToAlbums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabelToAlbums_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LabelToAlbums_Labels_LabelId",
                        column: x => x.LabelId,
                        principalTable: "Labels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlaylistPositionFix",
                columns: table => new
                {
                    PlaylistsId = table.Column<int>(type: "int", nullable: false),
                    PositionFixesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistPositionFix", x => new { x.PlaylistsId, x.PositionFixesId });
                    table.ForeignKey(
                        name: "FK_PlaylistPositionFix_Playlists_PlaylistsId",
                        column: x => x.PlaylistsId,
                        principalTable: "Playlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaylistPositionFix_PositionFixes_PositionFixesId",
                        column: x => x.PositionFixesId,
                        principalTable: "PositionFixes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LabelToAlbums_AlbumId",
                table: "LabelToAlbums",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_LabelToAlbums_LabelId",
                table: "LabelToAlbums",
                column: "LabelId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistPositionFix_PositionFixesId",
                table: "PlaylistPositionFix",
                column: "PositionFixesId");
        }
    }
}
