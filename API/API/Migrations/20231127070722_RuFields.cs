using Microsoft.EntityFrameworkCore.Migrations;

namespace Glomad.Migrations
{
    public partial class RuFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TextRu",
                table: "VisaDoc",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionRu",
                table: "Visa",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameRu",
                table: "Visa",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionRu",
                table: "NoVisaEntry",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TextRu",
                table: "VisaDoc");

            migrationBuilder.DropColumn(
                name: "DescriptionRu",
                table: "Visa");

            migrationBuilder.DropColumn(
                name: "NameRu",
                table: "Visa");

            migrationBuilder.DropColumn(
                name: "DescriptionRu",
                table: "NoVisaEntry");
        }
    }
}
