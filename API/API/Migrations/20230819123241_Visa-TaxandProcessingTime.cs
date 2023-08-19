using Microsoft.EntityFrameworkCore.Migrations;

namespace Glomad.Migrations
{
    public partial class VisaTaxandProcessingTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "ProcessingTimeTo",
                table: "Visa",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "ProcessingTimeUp",
                table: "Visa",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<string>(
                name: "TaxDescription",
                table: "Visa",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "TaxSize",
                table: "Visa",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcessingTimeTo",
                table: "Visa");

            migrationBuilder.DropColumn(
                name: "ProcessingTimeUp",
                table: "Visa");

            migrationBuilder.DropColumn(
                name: "TaxDescription",
                table: "Visa");

            migrationBuilder.DropColumn(
                name: "TaxSize",
                table: "Visa");
        }
    }
}
