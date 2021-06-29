using Microsoft.EntityFrameworkCore.Migrations;

namespace Glomad.Migrations
{
    public partial class VisaDocTableAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationFormUrl",
                table: "Embassy",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VisaDoc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmbassyId = table.Column<int>(type: "int", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisaDoc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisaDoc_Embassy_EmbassyId",
                        column: x => x.EmbassyId,
                        principalTable: "Embassy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VisaDoc_EmbassyId",
                table: "VisaDoc",
                column: "EmbassyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisaDoc");

            migrationBuilder.DropColumn(
                name: "ApplicationFormUrl",
                table: "Embassy");
        }
    }
}
