using Microsoft.EntityFrameworkCore.Migrations;

namespace Glomad.Migrations
{
    public partial class Embassy6NewFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Complexity",
                table: "Embassy",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Embassy",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "IssueDeadlines",
                table: "Embassy",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<bool>(
                name: "PermanentResidency",
                table: "Embassy",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Embassy",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "ProvidesСitizenship",
                table: "Embassy",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Complexity",
                table: "Embassy");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Embassy");

            migrationBuilder.DropColumn(
                name: "IssueDeadlines",
                table: "Embassy");

            migrationBuilder.DropColumn(
                name: "PermanentResidency",
                table: "Embassy");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Embassy");

            migrationBuilder.DropColumn(
                name: "ProvidesСitizenship",
                table: "Embassy");
        }
    }
}
