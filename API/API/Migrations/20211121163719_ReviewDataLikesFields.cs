using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Glomad.Migrations
{
    public partial class ReviewDataLikesFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Review",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Review",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Review");
        }
    }
}
