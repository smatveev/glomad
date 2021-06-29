using Microsoft.EntityFrameworkCore.Migrations;

namespace Glomad.Migrations
{
    public partial class CountryChangeCurrencyType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Country");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Country",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Country");

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "Country",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
