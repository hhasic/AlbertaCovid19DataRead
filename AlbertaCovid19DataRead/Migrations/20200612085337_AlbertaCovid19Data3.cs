using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlbertaCovid19DataRead.Migrations
{
    public partial class AlbertaCovid19Data3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    CasesArea = table.Column<int>(nullable: false),
                    Confirmed = table.Column<int>(nullable: true),
                    Active = table.Column<int>(nullable: true),
                    Recovered = table.Column<int>(nullable: true),
                    InHospital = table.Column<int>(nullable: true),
                    InIntensiveCare = table.Column<int>(nullable: true),
                    Deaths = table.Column<int>(nullable: true),
                    Tests = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cases");
        }
    }
}
