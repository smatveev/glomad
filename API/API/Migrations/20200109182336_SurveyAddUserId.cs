using Microsoft.EntityFrameworkCore.Migrations;

namespace Glomad.Migrations
{
    public partial class SurveyAddUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Survey",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Survey_UserId",
                table: "Survey",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Survey_AspNetUsers_UserId",
                table: "Survey",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Survey_AspNetUsers_UserId",
                table: "Survey");

            migrationBuilder.DropIndex(
                name: "IX_Survey_UserId",
                table: "Survey");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Survey");
        }
    }
}
