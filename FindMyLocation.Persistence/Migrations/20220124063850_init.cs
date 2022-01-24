using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FindMyLocation.Persistence.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FourSqaureVenues",
                columns: table => new
                {
                    locationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fsq_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    prefix = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    suffix = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    distance = table.Column<int>(type: "int", nullable: false),
                    latitude = table.Column<double>(type: "float", nullable: false),
                    longitude = table.Column<double>(type: "float", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cross_street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    locality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    po_box = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    post_town = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    postcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    locationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    timezone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FourSqaureVenues", x => x.locationId);
                });

            migrationBuilder.CreateTable(
                name: "ImageModels",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    prefix = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    suffix = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    width = table.Column<int>(type: "int", nullable: false),
                    height = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageModels", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ModelFours",
                columns: table => new
                {
                    locationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FourSqaureVenues");

            migrationBuilder.DropTable(
                name: "ImageModels");

            migrationBuilder.DropTable(
                name: "ModelFours");
        }
    }
}
