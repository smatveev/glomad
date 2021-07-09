using Microsoft.EntityFrameworkCore.Migrations;

namespace Glomad.Migrations
{
    public partial class VisaDocAddVisaId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VisaId",
                table: "VisaDoc",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VisaDoc_VisaId",
                table: "VisaDoc",
                column: "VisaId");

            migrationBuilder.AddForeignKey(
                name: "FK_VisaDoc_Visa_VisaId",
                table: "VisaDoc",
                column: "VisaId",
                principalTable: "Visa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisaDoc_Visa_VisaId",
                table: "VisaDoc");

            migrationBuilder.DropIndex(
                name: "IX_VisaDoc_VisaId",
                table: "VisaDoc");

            migrationBuilder.DropColumn(
                name: "VisaId",
                table: "VisaDoc");
        }
    }
}
