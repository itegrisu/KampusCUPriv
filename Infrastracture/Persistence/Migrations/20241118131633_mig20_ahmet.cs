using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mig20_ahmet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SaleDate",
                table: "VehicleTransactions",
                newName: "EndDate");

            migrationBuilder.AddColumn<int>(
                name: "EndKM",
                table: "VehicleTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndKM",
                table: "VehicleTransactions");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "VehicleTransactions",
                newName: "SaleDate");
        }
    }
}
