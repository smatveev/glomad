using Microsoft.EntityFrameworkCore.Migrations;

namespace Glomad.Migrations
{
    public partial class AmadeusRestrictions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AmadeusTravelRestrictions",
                table: "Country",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmadeusTravelRestrictions",
                table: "Country");
        }
    }
}
