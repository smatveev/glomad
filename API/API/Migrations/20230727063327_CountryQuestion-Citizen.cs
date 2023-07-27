using Microsoft.EntityFrameworkCore.Migrations;

namespace Glomad.Migrations
{
    public partial class CountryQuestionCitizen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CitizenId",
                table: "CountryQuestion",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CountryQuestion_CitizenId",
                table: "CountryQuestion",
                column: "CitizenId");

            migrationBuilder.AddForeignKey(
                name: "FK_CountryQuestion_Country_CitizenId",
                table: "CountryQuestion",
                column: "CitizenId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CountryQuestion_Country_CitizenId",
                table: "CountryQuestion");

            migrationBuilder.DropIndex(
                name: "IX_CountryQuestion_CitizenId",
                table: "CountryQuestion");

            migrationBuilder.DropColumn(
                name: "CitizenId",
                table: "CountryQuestion");
        }
    }
}
