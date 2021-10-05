using Microsoft.EntityFrameworkCore.Migrations;

namespace KMChartsUpdater.DAL.Migrations
{
    public partial class reportsFilename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Reports");
        }
    }
}
