using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Glomad.Migrations
{
    public partial class AmadeusApi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AmadeusApi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastCall = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CallsLimit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmadeusApi", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AmadeusApi");
        }
    }
}
