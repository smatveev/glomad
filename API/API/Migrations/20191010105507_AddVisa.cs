using Microsoft.EntityFrameworkCore.Migrations;

namespace Glomad.Migrations
{
    public partial class AddVisa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn("Title", "City", "Name");
            migrationBuilder.RenameColumn("Title", "Country", "Name");

            migrationBuilder.CreateTable(
                name: "Visa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    EmbassyId = table.Column<int>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Currency = table.Column<string>(nullable: true),
                    OnArrival = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visa_Embassy_EmbassyId",
                        column: x => x.EmbassyId,
                        principalTable: "Embassy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Visa_EmbassyId",
                table: "Visa",
                column: "EmbassyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Visa");

            migrationBuilder.RenameColumn("Name", "Country", "Title");
            migrationBuilder.RenameColumn("Name", "City", "Title");
        }
    }
}
