using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Glomad.Migrations
{
    public partial class AddSurvey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Survey",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(nullable: false),
                    Fun = table.Column<int>(nullable: false),
                    Internet = table.Column<byte>(nullable: false),
                    Safety = table.Column<byte>(nullable: false),
                    Friendly = table.Column<byte>(nullable: false),
                    StartupScore = table.Column<byte>(nullable: false),
                    CostOfLiving = table.Column<decimal>(nullable: false),
                    StayDuration = table.Column<byte>(nullable: false),
                    WantToComeBack = table.Column<bool>(nullable: false),
                    DateComeBack = table.Column<DateTime>(nullable: false),
                    Difficulties = table.Column<string>(maxLength: 1000, nullable: true),
                    Benefits = table.Column<string>(maxLength: 1000, nullable: true),
                    Drawbacks = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Survey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Survey_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Survey_CityId",
                table: "Survey",
                column: "CityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Survey");
        }
    }
}
