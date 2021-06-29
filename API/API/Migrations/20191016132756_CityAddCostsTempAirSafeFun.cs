using Microsoft.EntityFrameworkCore.Migrations;

namespace Glomad.Migrations
{
    public partial class CityAddCostsTempAirSafeFun : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AirQuality",
                table: "City",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "CostOfLiving",
                table: "City",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CostOfRent",
                table: "City",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<byte>(
                name: "Fun",
                table: "City",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "Safety",
                table: "City",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<short>(
                name: "Temperature",
                table: "City",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AirQuality",
                table: "City");

            migrationBuilder.DropColumn(
                name: "CostOfLiving",
                table: "City");

            migrationBuilder.DropColumn(
                name: "CostOfRent",
                table: "City");

            migrationBuilder.DropColumn(
                name: "Fun",
                table: "City");

            migrationBuilder.DropColumn(
                name: "Safety",
                table: "City");

            migrationBuilder.DropColumn(
                name: "Temperature",
                table: "City");
        }
    }
}
