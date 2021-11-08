using Microsoft.EntityFrameworkCore.Migrations;

namespace Glomad.Migrations
{
    public partial class CountryLetCountFieldsAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "LetEVisaCount",
                table: "Country",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "LetNoEntryVisaCount",
                table: "Country",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "LetVisaCount",
                table: "Country",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LetEVisaCount",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "LetNoEntryVisaCount",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "LetVisaCount",
                table: "Country");
        }
    }
}
