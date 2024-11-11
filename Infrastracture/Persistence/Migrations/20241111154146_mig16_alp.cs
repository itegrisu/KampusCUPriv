using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mig16_alp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Warehouses");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Warehouses",
                type: "varchar(150)",
                maxLength: 150,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Warehouses");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Warehouses",
                type: "varchar(250)",
                maxLength: 250,
                nullable: true);
        }
    }
}
