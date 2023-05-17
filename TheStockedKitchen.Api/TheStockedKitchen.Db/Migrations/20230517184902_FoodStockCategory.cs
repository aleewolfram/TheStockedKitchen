using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheStockedKitchen.DB.Migrations
{
    public partial class FoodStockCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                schema: "dbo",
                table: "FoodStock",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                schema: "dbo",
                table: "FoodStock");
        }
    }
}
