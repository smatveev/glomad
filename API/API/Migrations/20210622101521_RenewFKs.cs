using Microsoft.EntityFrameworkCore.Migrations;

namespace Glomad.Migrations
{
    public partial class RenewFKs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CityWeather_City_CityId",
                table: "CityWeather");

            migrationBuilder.DropForeignKey(
                name: "FK_ClimateData_City_CityId",
                table: "ClimateData");

            migrationBuilder.DropForeignKey(
                name: "FK_ClimateData_Country_CountryId",
                table: "ClimateData");

            migrationBuilder.DropForeignKey(
                name: "FK_Embassy_Country_CountryId",
                table: "Embassy");

            migrationBuilder.DropForeignKey(
                name: "FK_Power_Country_CountryId",
                table: "Power");

            migrationBuilder.DropForeignKey(
                name: "FK_Survey_City_CityId",
                table: "Survey");

            migrationBuilder.DropForeignKey(
                name: "FK_VisaAcceptance_Country_CountryId",
                table: "VisaAcceptance");

            migrationBuilder.DropForeignKey(
                name: "FK_VisaAcceptance_Visa_VisaId",
                table: "VisaAcceptance");

            migrationBuilder.AlterColumn<int>(
                name: "VisaId",
                table: "VisaAcceptance",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "VisaAcceptance",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Survey",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Power",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Embassy",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "ClimateData",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "ClimateData",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "CityWeather",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CityWeather_City_CityId",
                table: "CityWeather",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClimateData_City_CityId",
                table: "ClimateData",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClimateData_Country_CountryId",
                table: "ClimateData",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Embassy_Country_CountryId",
                table: "Embassy",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Power_Country_CountryId",
                table: "Power",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Survey_City_CityId",
                table: "Survey",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VisaAcceptance_Country_CountryId",
                table: "VisaAcceptance",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VisaAcceptance_Visa_VisaId",
                table: "VisaAcceptance",
                column: "VisaId",
                principalTable: "Visa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CityWeather_City_CityId",
                table: "CityWeather");

            migrationBuilder.DropForeignKey(
                name: "FK_ClimateData_City_CityId",
                table: "ClimateData");

            migrationBuilder.DropForeignKey(
                name: "FK_ClimateData_Country_CountryId",
                table: "ClimateData");

            migrationBuilder.DropForeignKey(
                name: "FK_Embassy_Country_CountryId",
                table: "Embassy");

            migrationBuilder.DropForeignKey(
                name: "FK_Power_Country_CountryId",
                table: "Power");

            migrationBuilder.DropForeignKey(
                name: "FK_Survey_City_CityId",
                table: "Survey");

            migrationBuilder.DropForeignKey(
                name: "FK_VisaAcceptance_Country_CountryId",
                table: "VisaAcceptance");

            migrationBuilder.DropForeignKey(
                name: "FK_VisaAcceptance_Visa_VisaId",
                table: "VisaAcceptance");

            migrationBuilder.AlterColumn<int>(
                name: "VisaId",
                table: "VisaAcceptance",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "VisaAcceptance",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Survey",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Power",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Embassy",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "ClimateData",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "ClimateData",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "CityWeather",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CityWeather_City_CityId",
                table: "CityWeather",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClimateData_City_CityId",
                table: "ClimateData",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClimateData_Country_CountryId",
                table: "ClimateData",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Embassy_Country_CountryId",
                table: "Embassy",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Power_Country_CountryId",
                table: "Power",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Survey_City_CityId",
                table: "Survey",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VisaAcceptance_Country_CountryId",
                table: "VisaAcceptance",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VisaAcceptance_Visa_VisaId",
                table: "VisaAcceptance",
                column: "VisaId",
                principalTable: "Visa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
