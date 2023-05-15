using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheStockedKitchen.DB.Migrations
{
    public partial class Units : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Unit",
                schema: "dbo",
                columns: table => new
                {
                    UnitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.UnitId);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Unit",
                columns: new[] { "UnitId", "Abbreviation", "Name" },
                values: new object[,]
                {
                    { 1, "MG", "Milligrams" },
                    { 2, "UG", "Micrograms" },
                    { 3, "G", "Grams" },
                    { 4, "IU", "International Units" },
                    { 5, "kJ", "Kilojoules" },
                    { 6, "KCAL", "Calories" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Unit",
                schema: "dbo");
        }
    }
}
