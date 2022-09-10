using Microsoft.EntityFrameworkCore.Migrations;

namespace Glomad.Migrations
{
    public partial class visaProgramUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProgramUrl",
                table: "Visa",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProgramUrl",
                table: "Visa");
        }
    }
}
