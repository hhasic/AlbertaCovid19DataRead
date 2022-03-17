using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlbertaCovid19DataRead.Migrations
{
    public partial class AlbertaCovid19Data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    AreaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    GeometryData = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.AreaId);
                });

            migrationBuilder.CreateTable(
                name: "LabTesting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    EdmontonZone = table.Column<int>(nullable: false),
                    CalgaryZone = table.Column<int>(nullable: false),
                    CentralZone = table.Column<int>(nullable: false),
                    NorthZone = table.Column<int>(nullable: false),
                    SouthZone = table.Column<int>(nullable: false),
                    UnknownZone = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabTesting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AreaCovidInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Cases = table.Column<int>(nullable: false),
                    Active = table.Column<int>(nullable: false),
                    Recovered = table.Column<int>(nullable: false),
                    Deaths = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaCovidInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AreaCovidInfo_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Area",
                        principalColumn: "AreaId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AreaCovidInfo_AreaId",
                table: "AreaCovidInfo",
                column: "AreaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AreaCovidInfo");

            migrationBuilder.DropTable(
                name: "LabTesting");

            migrationBuilder.DropTable(
                name: "Area");
        }
    }
}
