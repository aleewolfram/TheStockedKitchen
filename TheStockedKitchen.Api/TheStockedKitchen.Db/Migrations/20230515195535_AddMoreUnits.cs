using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheStockedKitchen.DB.Migrations
{
    public partial class AddMoreUnits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AllowedInDropDown",
                schema: "dbo",
                table: "Unit",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Unit",
                keyColumn: "UnitId",
                keyValue: 3,
                column: "AllowedInDropDown",
                value: true);

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Unit",
                columns: new[] { "UnitId", "Abbreviation", "AllowedInDropDown", "Name" },
                values: new object[,]
                {
                    { 7, "OZ", true, "Ounces" },
                    { 8, "C", true, "Cups" },
                    { 9, "LB", true, "Pounds" },
                    { 10, "TBSP", true, "Tablespoons" },
                    { 11, "TSP", true, "Teaspoons" },
                    { 12, "FL OZ", true, "Fluid Ounces" },
                    { 13, "PT", true, "Pints" },
                    { 14, "QT", true, "Quarts" },
                    { 15, "GAL", true, "Gallons" },
                    { 16, "W", true, "Whole" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Unit",
                keyColumn: "UnitId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Unit",
                keyColumn: "UnitId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Unit",
                keyColumn: "UnitId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Unit",
                keyColumn: "UnitId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Unit",
                keyColumn: "UnitId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Unit",
                keyColumn: "UnitId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Unit",
                keyColumn: "UnitId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Unit",
                keyColumn: "UnitId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Unit",
                keyColumn: "UnitId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Unit",
                keyColumn: "UnitId",
                keyValue: 16);

            migrationBuilder.DropColumn(
                name: "AllowedInDropDown",
                schema: "dbo",
                table: "Unit");
        }
    }
}
