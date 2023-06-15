using Microsoft.EntityFrameworkCore.Migrations;

namespace Glomad.Migrations
{
    public partial class NextCountriesField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NextCountries",
                table: "Country",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextCountries",
                table: "Country");
        }
    }
}
