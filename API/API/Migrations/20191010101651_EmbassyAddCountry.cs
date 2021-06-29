using Microsoft.EntityFrameworkCore.Migrations;

namespace Glomad.Migrations
{
    public partial class EmbassyAddCountry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Embassy",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Embassy_CountryId",
                table: "Embassy",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Embassy_Country_CountryId",
                table: "Embassy",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Embassy_Country_CountryId",
                table: "Embassy");

            migrationBuilder.DropIndex(
                name: "IX_Embassy_CountryId",
                table: "Embassy");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Embassy");
        }
    }
}
