using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MartinHobesaluChairMeditation.Data.Migrations
{
    public partial class AddCompletedOrderAmountToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompletedAmount",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedAmount",
                table: "Order");
        }
    }
}
