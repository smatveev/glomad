using Microsoft.EntityFrameworkCore.Migrations;

namespace Glomad.Migrations
{
    public partial class VisaTaxSizeUpTo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaxSize",
                table: "Visa",
                newName: "TaxSizeUp");

            migrationBuilder.AddColumn<byte>(
                name: "TaxSizeTo",
                table: "Visa",
                type: "tinyint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxSizeTo",
                table: "Visa");

            migrationBuilder.RenameColumn(
                name: "TaxSizeUp",
                table: "Visa",
                newName: "TaxSize");
        }
    }
}
