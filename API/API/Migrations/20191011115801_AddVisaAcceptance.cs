using Microsoft.EntityFrameworkCore.Migrations;

namespace Glomad.Migrations
{
    public partial class AddVisaAcceptance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VisaAcceptance",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisaId = table.Column<int>(nullable: false),
                    CountryId = table.Column<int>(nullable: false),
                    CountryIata = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisaAcceptance", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VisaAcceptance_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VisaAcceptance_Visa_VisaId",
                        column: x => x.VisaId,
                        principalTable: "Visa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VisaAcceptance_CountryId",
                table: "VisaAcceptance",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_VisaAcceptance_VisaId",
                table: "VisaAcceptance",
                column: "VisaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisaAcceptance");
        }
    }
}
