using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MartinHobesaluChairMeditation.Data.Migrations
{
    public partial class addOrdererName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrdererName",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrdererName",
                table: "Order");
        }
    }
}
