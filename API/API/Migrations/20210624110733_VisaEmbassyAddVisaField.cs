using Microsoft.EntityFrameworkCore.Migrations;

namespace Glomad.Migrations
{
    public partial class VisaEmbassyAddVisaField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VisaId",
                table: "VisaEmbassy",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VisaEmbassy_VisaId",
                table: "VisaEmbassy",
                column: "VisaId");

            migrationBuilder.AddForeignKey(
                name: "FK_VisaEmbassy_Visa_VisaId",
                table: "VisaEmbassy",
                column: "VisaId",
                principalTable: "Visa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisaEmbassy_Visa_VisaId",
                table: "VisaEmbassy");

            migrationBuilder.DropIndex(
                name: "IX_VisaEmbassy_VisaId",
                table: "VisaEmbassy");

            migrationBuilder.DropColumn(
                name: "VisaId",
                table: "VisaEmbassy");
        }
    }
}
