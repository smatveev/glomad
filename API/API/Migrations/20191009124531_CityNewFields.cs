using Microsoft.EntityFrameworkCore.Migrations;

namespace Glomad.Migrations
{
    public partial class CityNewFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CountryIata",
                table: "City",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Iata",
                table: "City",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "City",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "City",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TimeZone",
                table: "City",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryIata",
                table: "City");

            migrationBuilder.DropColumn(
                name: "Iata",
                table: "City");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "City");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "City");

            migrationBuilder.DropColumn(
                name: "TimeZone",
                table: "City");
        }
    }
}
