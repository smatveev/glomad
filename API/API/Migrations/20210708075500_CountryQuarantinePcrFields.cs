using Microsoft.EntityFrameworkCore.Migrations;

namespace Glomad.Migrations
{
    public partial class CountryQuarantinePcrFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AcceptableVaccinations",
                table: "Country",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPcrRequired",
                table: "Country",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsQuarantine",
                table: "Country",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PCR",
                table: "Country",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Quarantine",
                table: "Country",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptableVaccinations",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "IsPcrRequired",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "IsQuarantine",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "PCR",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "Quarantine",
                table: "Country");
        }
    }
}
