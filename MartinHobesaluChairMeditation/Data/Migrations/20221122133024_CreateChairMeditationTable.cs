using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MartinHobesaluChairMeditation.Data.Migrations
{
    public partial class CreateChairMeditationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChairMeditation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderAmount = table.Column<int>(type: "int", nullable: false),
                    CompleteAmount = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChairMeditation", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChairMeditation");
        }
    }
}
