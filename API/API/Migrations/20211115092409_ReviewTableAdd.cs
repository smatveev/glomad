using Microsoft.EntityFrameworkCore.Migrations;

namespace Glomad.Migrations
{
    public partial class ReviewTableAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisaId = table.Column<int>(type: "int", nullable: true),
                    EmbassyId = table.Column<int>(type: "int", nullable: true),
                    Simplicity = table.Column<byte>(type: "tinyint", nullable: false),
                    Waiting = table.Column<byte>(type: "tinyint", nullable: false),
                    Loyalty = table.Column<byte>(type: "tinyint", nullable: false),
                    IsObtained = table.Column<bool>(type: "bit", nullable: false),
                    Pros = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cons = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Review_Embassy_EmbassyId",
                        column: x => x.EmbassyId,
                        principalTable: "Embassy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Review_Visa_VisaId",
                        column: x => x.VisaId,
                        principalTable: "Visa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Review_EmbassyId",
                table: "Review",
                column: "EmbassyId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_VisaId",
                table: "Review",
                column: "VisaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Review");
        }
    }
}
