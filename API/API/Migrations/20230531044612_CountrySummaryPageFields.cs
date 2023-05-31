using Microsoft.EntityFrameworkCore.Migrations;

namespace Glomad.Migrations
{
    public partial class CountrySummaryPageFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BestSeason",
                table: "Country",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyDescription",
                table: "Country",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DriverLicense",
                table: "Country",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DriverRules",
                table: "Country",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmrgencyNumbers",
                table: "Country",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Etiquette",
                table: "Country",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Internet",
                table: "Country",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobileOperators",
                table: "Country",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PopularPlaces",
                table: "Country",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Safety",
                table: "Country",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "SafetyLevel",
                table: "Country",
                type: "tinyint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "Country",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Transport",
                table: "Country",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Vaccinations",
                table: "Country",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BestSeason",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "CurrencyDescription",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "DriverLicense",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "DriverRules",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "EmrgencyNumbers",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "Etiquette",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "Internet",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "MobileOperators",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "PopularPlaces",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "Safety",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "SafetyLevel",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "Summary",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "Transport",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "Vaccinations",
                table: "Country");
        }
    }
}
