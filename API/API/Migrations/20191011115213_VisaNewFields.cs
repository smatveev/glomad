using Microsoft.EntityFrameworkCore.Migrations;

namespace Glomad.Migrations
{
    public partial class VisaNewFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CountryIata",
                table: "Visa",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Visa",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "Duration",
                table: "Visa",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.CreateIndex(
                name: "IX_Visa_CountryId",
                table: "Visa",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visa_Country_CountryId",
                table: "Visa",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visa_Country_CountryId",
                table: "Visa");

            migrationBuilder.DropIndex(
                name: "IX_Visa_CountryId",
                table: "Visa");

            migrationBuilder.DropColumn(
                name: "CountryIata",
                table: "Visa");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Visa");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Visa");
        }
    }
}
