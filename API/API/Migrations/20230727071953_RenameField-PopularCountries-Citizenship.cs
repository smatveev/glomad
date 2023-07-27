using Microsoft.EntityFrameworkCore.Migrations;

namespace Glomad.Migrations
{
    public partial class RenameFieldPopularCountriesCitizenship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PopularCountries_Country_СitizenshipId",
                table: "PopularCountries");

            migrationBuilder.RenameColumn(
                name: "СitizenshipId",
                table: "PopularCountries",
                newName: "CitizenshipId");

            migrationBuilder.RenameIndex(
                name: "IX_PopularCountries_СitizenshipId",
                table: "PopularCountries",
                newName: "IX_PopularCountries_CitizenshipId");

            migrationBuilder.AddForeignKey(
                name: "FK_PopularCountries_Country_CitizenshipId",
                table: "PopularCountries",
                column: "CitizenshipId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PopularCountries_Country_CitizenshipId",
                table: "PopularCountries");

            migrationBuilder.RenameColumn(
                name: "CitizenshipId",
                table: "PopularCountries",
                newName: "СitizenshipId");

            migrationBuilder.RenameIndex(
                name: "IX_PopularCountries_CitizenshipId",
                table: "PopularCountries",
                newName: "IX_PopularCountries_СitizenshipId");

            migrationBuilder.AddForeignKey(
                name: "FK_PopularCountries_Country_СitizenshipId",
                table: "PopularCountries",
                column: "СitizenshipId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
