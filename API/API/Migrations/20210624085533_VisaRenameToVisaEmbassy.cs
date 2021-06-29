using Microsoft.EntityFrameworkCore.Migrations;

namespace Glomad.Migrations
{
    public partial class VisaRenameToVisaEmbassy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisaAcceptance_Visa_VisaId",
                table: "VisaAcceptance");
            migrationBuilder.DropIndex("IX_Visa_CountryId", "Visa");
            migrationBuilder.DropIndex("IX_Visa_EmbassyId", "Visa");

            migrationBuilder.RenameTable("Visa", null, "VisaEmbassy", null);

            migrationBuilder.CreateIndex(
                name: "IX_VisaEmbassy_CountryId",
                table: "VisaEmbassy",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_VisaEmbassy_EmbassyId",
                table: "VisaEmbassy",
                column: "EmbassyId");

            migrationBuilder.AddForeignKey(
                name: "FK_VisaAcceptance_VisaEmbassy_VisaEmbassyId",
                table: "VisaAcceptance",
                column: "VisaId",
                principalTable: "VisaEmbassy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisaAcceptance_VisaEmbassy_VisaId",
                table: "VisaAcceptance");

            migrationBuilder.DropIndex("IX_VisaEmbassy_CountryId", "VisaEmbassy");
            migrationBuilder.DropIndex("IX_VisaEmbassy_EmbassyId", "VisaEmbassy");

            migrationBuilder.RenameTable("VisaEmbassy", null, "Visa", null);

            migrationBuilder.CreateIndex(
                name: "IX_Visa_CountryId",
                table: "Visa",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Visa_EmbassyId",
                table: "Visa",
                column: "EmbassyId");

            migrationBuilder.AddForeignKey(
                name: "FK_VisaAcceptance_Visa_VisaId",
                table: "VisaAcceptance",
                column: "VisaId",
                principalTable: "Visa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
