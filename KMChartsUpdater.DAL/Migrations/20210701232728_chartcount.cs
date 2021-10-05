using Microsoft.EntityFrameworkCore.Migrations;

namespace KMChartsUpdater.DAL.Migrations
{
    public partial class chartcount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Charts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Charts");
        }
    }
}
