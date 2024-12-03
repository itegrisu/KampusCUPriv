using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mig30_ahmet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefNoTransportation",
                table: "TransportationServices",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefNoTransportationPassenger",
                table: "TransportationPassengers",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefNoTransportationGroup",
                table: "TransportationGroups",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefNoTransportation",
                table: "TransportationServices");

            migrationBuilder.DropColumn(
                name: "RefNoTransportationPassenger",
                table: "TransportationPassengers");

            migrationBuilder.DropColumn(
                name: "RefNoTransportationGroup",
                table: "TransportationGroups");
        }
    }
}
