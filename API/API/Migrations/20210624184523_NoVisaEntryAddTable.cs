using Microsoft.EntityFrameworkCore.Migrations;

namespace Glomad.Migrations
{
    public partial class NoVisaEntryAddTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NoVisaEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryPassportId = table.Column<int>(type: "int", nullable: true),
                    CountryDestinationId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoVisaEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NoVisaEntry_Country_CountryDestinationId",
                        column: x => x.CountryDestinationId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NoVisaEntry_Country_CountryPassportId",
                        column: x => x.CountryPassportId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NoVisaEntry_CountryDestinationId",
                table: "NoVisaEntry",
                column: "CountryDestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_NoVisaEntry_CountryPassportId",
                table: "NoVisaEntry",
                column: "CountryPassportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NoVisaEntry");
        }
    }
}
