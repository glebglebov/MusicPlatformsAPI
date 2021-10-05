using Microsoft.EntityFrameworkCore.Migrations;

namespace KMChartsUpdater.DAL.Migrations
{
    public partial class poschart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChartId",
                table: "PositionFixes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PositionFixes_ChartId",
                table: "PositionFixes",
                column: "ChartId");

            migrationBuilder.AddForeignKey(
                name: "FK_PositionFixes_Charts_ChartId",
                table: "PositionFixes",
                column: "ChartId",
                principalTable: "Charts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PositionFixes_Charts_ChartId",
                table: "PositionFixes");

            migrationBuilder.DropIndex(
                name: "IX_PositionFixes_ChartId",
                table: "PositionFixes");

            migrationBuilder.DropColumn(
                name: "ChartId",
                table: "PositionFixes");
        }
    }
}
