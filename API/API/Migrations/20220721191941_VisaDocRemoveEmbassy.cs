using Microsoft.EntityFrameworkCore.Migrations;

namespace Glomad.Migrations
{
    public partial class VisaDocRemoveEmbassy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisaDoc_Embassy_EmbassyId",
                table: "VisaDoc");

            migrationBuilder.DropIndex(
                name: "IX_VisaDoc_EmbassyId",
                table: "VisaDoc");

            migrationBuilder.DropColumn(
                name: "EmbassyId",
                table: "VisaDoc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmbassyId",
                table: "VisaDoc",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VisaDoc_EmbassyId",
                table: "VisaDoc",
                column: "EmbassyId");

            migrationBuilder.AddForeignKey(
                name: "FK_VisaDoc_Embassy_EmbassyId",
                table: "VisaDoc",
                column: "EmbassyId",
                principalTable: "Embassy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
