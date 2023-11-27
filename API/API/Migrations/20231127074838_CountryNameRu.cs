using Microsoft.EntityFrameworkCore.Migrations;

namespace Glomad.Migrations
{
    public partial class CountryNameRu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameRu",
                table: "Country",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameRu",
                table: "Country");
        }
    }
}
