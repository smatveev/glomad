using Microsoft.EntityFrameworkCore.Migrations;

namespace Glomad.Migrations
{
    public partial class EmbassyOriginalCountryAddField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OriginalCountryId",
                table: "Embassy",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Embassy_OriginalCountryId",
                table: "Embassy",
                column: "OriginalCountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Embassy_Country_OriginalCountryId",
                table: "Embassy",
                column: "OriginalCountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Embassy_Country_OriginalCountryId",
                table: "Embassy");

            migrationBuilder.DropIndex(
                name: "IX_Embassy_OriginalCountryId",
                table: "Embassy");

            migrationBuilder.DropColumn(
                name: "OriginalCountryId",
                table: "Embassy");
        }
    }
}
