using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mig38 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationHotelStaffs_SCCompanies_GidReservationHotelFK",
                table: "ReservationHotelStaffs");

            migrationBuilder.RenameColumn(
                name: "GidReservationHotelFK",
                table: "ReservationHotelStaffs",
                newName: "GidHotelFK");

            migrationBuilder.RenameIndex(
                name: "IX_ReservationHotelStaffs_GidReservationHotelFK",
                table: "ReservationHotelStaffs",
                newName: "IX_ReservationHotelStaffs_GidHotelFK");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationHotelStaffs_SCCompanies_GidHotelFK",
                table: "ReservationHotelStaffs",
                column: "GidHotelFK",
                principalTable: "SCCompanies",
                principalColumn: "Gid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationHotelStaffs_SCCompanies_GidHotelFK",
                table: "ReservationHotelStaffs");

            migrationBuilder.RenameColumn(
                name: "GidHotelFK",
                table: "ReservationHotelStaffs",
                newName: "GidReservationHotelFK");

            migrationBuilder.RenameIndex(
                name: "IX_ReservationHotelStaffs_GidHotelFK",
                table: "ReservationHotelStaffs",
                newName: "IX_ReservationHotelStaffs_GidReservationHotelFK");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationHotelStaffs_SCCompanies_GidReservationHotelFK",
                table: "ReservationHotelStaffs",
                column: "GidReservationHotelFK",
                principalTable: "SCCompanies",
                principalColumn: "Gid",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
