using Microsoft.EntityFrameworkCore.Migrations;

namespace KMChartsUpdater.DAL.Migrations
{
    public partial class Large : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Charts_ChartId",
                table: "Albums");

            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Labels_LabelId",
                table: "Albums");

            migrationBuilder.DropForeignKey(
                name: "FK_Audios_Charts_ChartId",
                table: "Audios");

            migrationBuilder.DropForeignKey(
                name: "FK_Audios_Labels_LabelId",
                table: "Audios");

            migrationBuilder.DropIndex(
                name: "IX_Audios_ChartId",
                table: "Audios");

            migrationBuilder.DropIndex(
                name: "IX_Audios_LabelId",
                table: "Audios");

            migrationBuilder.DropIndex(
                name: "IX_Albums_ChartId",
                table: "Albums");

            migrationBuilder.DropIndex(
                name: "IX_Albums_LabelId",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "ChartId",
                table: "Audios");

            migrationBuilder.DropColumn(
                name: "LabelId",
                table: "Audios");

            migrationBuilder.DropColumn(
                name: "ChartId",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "LabelId",
                table: "Albums");

            migrationBuilder.AddColumn<int>(
                name: "PlatformId",
                table: "Charts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LabelToAlbums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabelId = table.Column<int>(type: "int", nullable: true),
                    AlbumId = table.Column<int>(type: "int", nullable: true)
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
                name: "LabelToAudios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabelId = table.Column<int>(type: "int", nullable: true),
                    AudioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabelToAudios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabelToAudios_Audios_AudioId",
                        column: x => x.AudioId,
                        principalTable: "Audios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LabelToAudios_Labels_LabelId",
                        column: x => x.LabelId,
                        principalTable: "Labels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Platforms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platforms", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Charts_PlatformId",
                table: "Charts",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_LabelToAlbums_AlbumId",
                table: "LabelToAlbums",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_LabelToAlbums_LabelId",
                table: "LabelToAlbums",
                column: "LabelId");

            migrationBuilder.CreateIndex(
                name: "IX_LabelToAudios_AudioId",
                table: "LabelToAudios",
                column: "AudioId");

            migrationBuilder.CreateIndex(
                name: "IX_LabelToAudios_LabelId",
                table: "LabelToAudios",
                column: "LabelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Charts_Platforms_PlatformId",
                table: "Charts",
                column: "PlatformId",
                principalTable: "Platforms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Charts_Platforms_PlatformId",
                table: "Charts");

            migrationBuilder.DropTable(
                name: "LabelToAlbums");

            migrationBuilder.DropTable(
                name: "LabelToAudios");

            migrationBuilder.DropTable(
                name: "Platforms");

            migrationBuilder.DropIndex(
                name: "IX_Charts_PlatformId",
                table: "Charts");

            migrationBuilder.DropColumn(
                name: "PlatformId",
                table: "Charts");

            migrationBuilder.AddColumn<int>(
                name: "ChartId",
                table: "Audios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LabelId",
                table: "Audios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChartId",
                table: "Albums",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LabelId",
                table: "Albums",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Audios_ChartId",
                table: "Audios",
                column: "ChartId");

            migrationBuilder.CreateIndex(
                name: "IX_Audios_LabelId",
                table: "Audios",
                column: "LabelId");

            migrationBuilder.CreateIndex(
                name: "IX_Albums_ChartId",
                table: "Albums",
                column: "ChartId");

            migrationBuilder.CreateIndex(
                name: "IX_Albums_LabelId",
                table: "Albums",
                column: "LabelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Charts_ChartId",
                table: "Albums",
                column: "ChartId",
                principalTable: "Charts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Labels_LabelId",
                table: "Albums",
                column: "LabelId",
                principalTable: "Labels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Audios_Charts_ChartId",
                table: "Audios",
                column: "ChartId",
                principalTable: "Charts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Audios_Labels_LabelId",
                table: "Audios",
                column: "LabelId",
                principalTable: "Labels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
