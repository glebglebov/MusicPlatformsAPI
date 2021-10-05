using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KMChartsUpdater.DAL.Migrations
{
    public partial class GlobalFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GlobalFixId",
                table: "ChartFixes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GlobalFixes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateString = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalFixes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChartFixes_GlobalFixId",
                table: "ChartFixes",
                column: "GlobalFixId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChartFixes_GlobalFixes_GlobalFixId",
                table: "ChartFixes",
                column: "GlobalFixId",
                principalTable: "GlobalFixes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChartFixes_GlobalFixes_GlobalFixId",
                table: "ChartFixes");

            migrationBuilder.DropTable(
                name: "GlobalFixes");

            migrationBuilder.DropIndex(
                name: "IX_ChartFixes_GlobalFixId",
                table: "ChartFixes");

            migrationBuilder.DropColumn(
                name: "GlobalFixId",
                table: "ChartFixes");
        }
    }
}
