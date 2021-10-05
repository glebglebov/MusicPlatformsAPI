using Microsoft.EntityFrameworkCore.Migrations;

namespace KMChartsUpdater.DAL.Migrations
{
    public partial class ChartItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlbumPositionFixes");

            migrationBuilder.RenameColumn(
                name: "CoverbUrl",
                table: "Albums",
                newName: "CoverUrl");

            migrationBuilder.AddColumn<int>(
                name: "AlbumId",
                table: "PositionFixes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PositionFixes_AlbumId",
                table: "PositionFixes",
                column: "AlbumId");

            migrationBuilder.AddForeignKey(
                name: "FK_PositionFixes_Albums_AlbumId",
                table: "PositionFixes",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PositionFixes_Albums_AlbumId",
                table: "PositionFixes");

            migrationBuilder.DropIndex(
                name: "IX_PositionFixes_AlbumId",
                table: "PositionFixes");

            migrationBuilder.DropColumn(
                name: "AlbumId",
                table: "PositionFixes");

            migrationBuilder.RenameColumn(
                name: "CoverUrl",
                table: "Albums",
                newName: "CoverbUrl");

            migrationBuilder.CreateTable(
                name: "AlbumPositionFixes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlbumId = table.Column<int>(type: "int", nullable: true),
                    ChartFixId = table.Column<int>(type: "int", nullable: true),
                    IsNew = table.Column<bool>(type: "bit", nullable: false),
                    Plays = table.Column<long>(type: "bigint", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    Shift = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumPositionFixes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlbumPositionFixes_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlbumPositionFixes_ChartFixes_ChartFixId",
                        column: x => x.ChartFixId,
                        principalTable: "ChartFixes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlbumPositionFixes_AlbumId",
                table: "AlbumPositionFixes",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_AlbumPositionFixes_ChartFixId",
                table: "AlbumPositionFixes",
                column: "ChartFixId");
        }
    }
}
