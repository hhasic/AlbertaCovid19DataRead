using Microsoft.EntityFrameworkCore.Migrations;

namespace AlbertaCovid19DataRead.Migrations
{
    public partial class AlbertaCovid19Data2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AreaType",
                table: "Area",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AreaType",
                table: "Area");
        }
    }
}
