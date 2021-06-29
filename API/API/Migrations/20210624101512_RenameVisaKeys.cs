using Microsoft.EntityFrameworkCore.Migrations;

namespace Glomad.Migrations
{
    public partial class RenameVisaKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_VisaAcceptance_VisaEmbassy_VisaId", "VisaAcceptance");

            migrationBuilder.DropPrimaryKey("PK_Visa", "VisaEmbassy");
            migrationBuilder.DropForeignKey("FK_Visa_Embassy_EmbassyId", "VisaEmbassy");
            migrationBuilder.DropForeignKey("FK_Visa_Country_CountryId", "VisaEmbassy");

            migrationBuilder.AddPrimaryKey("PK_VisaEmbassy", "VisaEmbassy", "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VisaEmbassy_Embassy_EmbassyId",
                table: "VisaEmbassy",
                column: "EmbassyId",
                principalTable: "Embassy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VisaEmbassy_Country_CountryId",
                table: "VisaEmbassy",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VisaAcceptance_VisaEmbassy_VisaId",
                table: "VisaEmbassy",
                column: "EmbassyId",
                principalTable: "Embassy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
