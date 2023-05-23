using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheStockedKitchen.DB.Migrations
{
    public partial class UnitConversion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UnitConversion",
                schema: "dbo",
                columns: table => new
                {
                    UnitConversionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitAbbreviation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitAmount = table.Column<double>(type: "float", nullable: false),
                    CompareUnitAbbreviation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompareUnitAmount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitConversion", x => x.UnitConversionId);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "UnitConversion",
                columns: new[] { "UnitConversionId", "CompareUnitAbbreviation", "CompareUnitAmount", "UnitAbbreviation", "UnitAmount" },
                values: new object[,]
                {
                    { 1, "G", 1.0, "G", 1.0 },
                    { 2, "OZ", 0.035274, "G", 1.0 },
                    { 3, "C", 0.0042267528380000004, "G", 1.0 },
                    { 4, "LB", 0.00220462, "G", 1.0 },
                    { 5, "TBSP", 0.067628045400000003, "G", 1.0 },
                    { 6, "TSP", 0.20288413620000001, "G", 1.0 },
                    { 7, "FL OZ", 29.57, "G", 1.0 },
                    { 8, "PT", 0.0021133764190000002, "G", 1.0 },
                    { 9, "QT", 0.0010566882090000001, "G", 1.0 },
                    { 10, "GAL", 0.00026417205200000003, "G", 1.0 },
                    { 11, "G", 28.349499999999999, "OZ", 1.0 },
                    { 12, "OZ", 1.0, "OZ", 1.0 },
                    { 13, "C", 0.119826427325, "OZ", 1.0 },
                    { 14, "LB", 0.0625, "OZ", 1.0 },
                    { 15, "TBSP", 2.0, "OZ", 1.0 },
                    { 16, "TSP", 6.0, "OZ", 1.0 },
                    { 17, "FL OZ", 0.95861141853499998, "OZ", 1.0 },
                    { 18, "PT", 0.052042100000000001, "OZ", 1.0 },
                    { 19, "QT", 0.03125, "OZ", 1.0 },
                    { 20, "GAL", 0.0078125, "OZ", 1.0 },
                    { 21, "G", 236.58823648491, "C", 1.0 },
                    { 22, "OZ", 8.3454044514869992, "C", 1.0 },
                    { 23, "C", 1.0, "C", 1.0 },
                    { 24, "LB", 0.52158777821799995, "C", 1.0 },
                    { 25, "TBSP", 16.0, "C", 1.0 },
                    { 26, "TSP", 48.0, "C", 1.0 },
                    { 27, "FL OZ", 8.0, "C", 1.0 },
                    { 28, "PT", 0.5, "C", 1.0 },
                    { 29, "QT", 0.25, "C", 1.0 },
                    { 30, "GAL", 0.0625, "C", 1.0 },
                    { 31, "G", 453.59237000000002, "LB", 1.0 },
                    { 32, "OZ", 16.0, "LB", 1.0 },
                    { 33, "C", 1.917222837193, "LB", 1.0 },
                    { 34, "LB", 1.0, "LB", 1.0 },
                    { 35, "TBSP", 30.675565391454001, "LB", 1.0 },
                    { 36, "TSP", 92.026696174362002, "LB", 1.0 },
                    { 37, "FL OZ", 15.337782696563, "LB", 1.0 },
                    { 38, "PT", 0.95861141853499998, "LB", 1.0 },
                    { 39, "QT", 0.47930570926799998, "LB", 1.0 },
                    { 40, "GAL", 0.11982642731699999, "LB", 1.0 },
                    { 41, "G", 14.786764782056, "TBSP", 1.0 },
                    { 42, "OZ", 0.52158777828000003, "TBSP", 1.0 }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "UnitConversion",
                columns: new[] { "UnitConversionId", "CompareUnitAbbreviation", "CompareUnitAmount", "UnitAbbreviation", "UnitAmount" },
                values: new object[,]
                {
                    { 43, "C", 0.0625, "TBSP", 1.0 },
                    { 44, "LB", 0.032599236141999999, "TBSP", 1.0 },
                    { 45, "TBSP", 1.0, "TBSP", 1.0 },
                    { 46, "TSP", 3.0, "TBSP", 1.0 },
                    { 47, "FL OZ", 0.5, "TBSP", 1.0 },
                    { 48, "PT", 0.03125, "TBSP", 1.0 },
                    { 49, "QT", 0.015625, "TBSP", 1.0 },
                    { 50, "GAL", 0.0039060000000000002, "TBSP", 1.0 },
                    { 51, "G", 4.9289215940190001, "TSP", 1.0 },
                    { 52, "OZ", 0.17386259275999999, "TSP", 1.0 },
                    { 53, "C", 0.020833000000000001, "TSP", 1.0 },
                    { 54, "LB", 0.010866412047, "TSP", 1.0 },
                    { 55, "TBSP", 0.33333299999999999, "TSP", 1.0 },
                    { 56, "TSP", 1.0, "TSP", 1.0 },
                    { 57, "FL OZ", 0.16666700000000001, "TSP", 1.0 },
                    { 58, "PT", 0.010416999999999999, "TSP", 1.0 },
                    { 59, "QT", 0.005208, "TSP", 1.0 },
                    { 60, "GAL", 0.001302, "TSP", 1.0 },
                    { 61, "G", 29.573529562499999, "FL OZ", 1.0 },
                    { 62, "OZ", 1.043175556502, "FL OZ", 1.0 },
                    { 63, "C", 0.125, "FL OZ", 1.0 },
                    { 64, "LB", 0.065198472280999994, "FL OZ", 1.0 },
                    { 65, "TBSP", 2.0, "FL OZ", 1.0 },
                    { 66, "TSP", 6.0, "FL OZ", 1.0 },
                    { 67, "FL OZ", 1.0, "FL OZ", 1.0 },
                    { 68, "PT", 0.0625, "FL OZ", 1.0 },
                    { 69, "QT", 0.03125, "FL OZ", 1.0 },
                    { 70, "GAL", 0.0078120000000000004, "FL OZ", 1.0 },
                    { 71, "G", 473.17647299999999, "PT", 1.0 },
                    { 72, "OZ", 16.690808904038999, "PT", 1.0 },
                    { 73, "C", 2.0, "PT", 1.0 },
                    { 74, "LB", 1.043175556502, "PT", 1.0 },
                    { 75, "TBSP", 32.0, "PT", 1.0 },
                    { 76, "TSP", 96.0, "PT", 1.0 },
                    { 77, "FL OZ", 16.0, "PT", 1.0 },
                    { 78, "PT", 1.0, "PT", 1.0 },
                    { 79, "QT", 0.5, "PT", 1.0 },
                    { 80, "GAL", 0.125, "PT", 1.0 },
                    { 81, "G", 946.35294599998997, "QT", 1.0 },
                    { 82, "OZ", 33.381617808077003, "QT", 1.0 },
                    { 83, "C", 4.0, "QT", 1.0 },
                    { 84, "LB", 2.0863511130050001, "QT", 1.0 }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "UnitConversion",
                columns: new[] { "UnitConversionId", "CompareUnitAbbreviation", "CompareUnitAmount", "UnitAbbreviation", "UnitAmount" },
                values: new object[,]
                {
                    { 85, "TBSP", 64.0, "QT", 1.0 },
                    { 86, "TSP", 192.0, "QT", 1.0 },
                    { 87, "FL OZ", 32.0, "QT", 1.0 },
                    { 88, "PT", 2.0, "QT", 1.0 },
                    { 89, "QT", 1.0, "QT", 1.0 },
                    { 90, "GAL", 0.25, "QT", 1.0 },
                    { 91, "G", 3785.4117840007002, "GAL", 1.0 },
                    { 92, "OZ", 133.52647123233001, "GAL", 1.0 },
                    { 93, "C", 16.0, "GAL", 1.0 },
                    { 94, "LB", 8.3454044520210005, "GAL", 1.0 },
                    { 95, "TBSP", 256.0, "GAL", 1.0 },
                    { 96, "TSP", 768.0, "GAL", 1.0 },
                    { 97, "FL OZ", 128.0, "GAL", 1.0 },
                    { 98, "PT", 8.0, "GAL", 1.0 },
                    { 99, "QT", 4.0, "GAL", 1.0 },
                    { 100, "GAL", 1.0, "GAL", 1.0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnitConversion",
                schema: "dbo");
        }
    }
}
