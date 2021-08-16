using Microsoft.EntityFrameworkCore.Migrations;

namespace Vehicles.API.Migrations
{
    public partial class AddPriceToProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Procedures",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Procedures");
        }
    }
}
