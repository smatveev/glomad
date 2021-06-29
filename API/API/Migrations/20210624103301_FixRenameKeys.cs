using Microsoft.EntityFrameworkCore.Migrations;

namespace Glomad.Migrations
{
    public partial class FixRenameKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_VisaAcceptance_VisaEmbassy_VisaId", "VisaEmbassy");

            migrationBuilder.AddForeignKey(
                name: "FK_VisaAcceptance_VisaEmbassy_VisaId",
                table: "VisaAcceptance",
                column: "VisaId",
                principalTable: "VisaEmbassy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
