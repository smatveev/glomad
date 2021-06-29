using Microsoft.EntityFrameworkCore.Migrations;

namespace Glomad.Migrations
{
    public partial class FeedbackAddCountry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Feedback",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_CountryId",
                table: "Feedback",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_Country_CountryId",
                table: "Feedback",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_Country_CountryId",
                table: "Feedback");

            migrationBuilder.DropIndex(
                name: "IX_Feedback_CountryId",
                table: "Feedback");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Feedback");
        }
    }
}
