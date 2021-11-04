using Microsoft.EntityFrameworkCore.Migrations;

namespace Glomad.Migrations
{
    public partial class NoVisaEntryIsEvisaAvailIsVisaReqFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEVisaAvailable",
                table: "NoVisaEntry",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisaRequired",
                table: "NoVisaEntry",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEVisaAvailable",
                table: "NoVisaEntry");

            migrationBuilder.DropColumn(
                name: "IsVisaRequired",
                table: "NoVisaEntry");
        }
    }
}
