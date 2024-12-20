using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mig36 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Guests",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidNationalityFK = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Surename = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Duty = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Institution = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Phone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    HesCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_Guests_Countries_GidNationalityFK",
                        column: x => x.GidNationalityFK,
                        principalTable: "Countries",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReservationHotelStaffs",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidHotelFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    GsmNo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    HotelStaffStatus = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    PasswordHash = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationHotelStaffs", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_ReservationHotelStaffs_SCCompanies_GidHotelFK",
                        column: x => x.GidHotelFK,
                        principalTable: "SCCompanies",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidOrganizationFK = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EstimatedGuestCount = table.Column<int>(type: "int", nullable: true),
                    EstimatedAccommodationCount = table.Column<int>(type: "int", nullable: true),
                    ReservationType = table.Column<int>(type: "int", nullable: false),
                    ReservationStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_Reservations_Organizations_GidOrganizationFK",
                        column: x => x.GidOrganizationFK,
                        principalTable: "Organizations",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReservationHotels",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidReservationFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidHotelFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidBuyCurrencyTypeFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidSellCurrencyTypeFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationHotels", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_ReservationHotels_Currencies_GidBuyCurrencyTypeFK",
                        column: x => x.GidBuyCurrencyTypeFK,
                        principalTable: "Currencies",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservationHotels_Currencies_GidSellCurrencyTypeFK",
                        column: x => x.GidSellCurrencyTypeFK,
                        principalTable: "Currencies",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservationHotels_Reservations_GidReservationFK",
                        column: x => x.GidReservationFK,
                        principalTable: "Reservations",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservationHotels_SCCompanies_GidHotelFK",
                        column: x => x.GidHotelFK,
                        principalTable: "SCCompanies",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReservationDetails",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidReservationHotelFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidRoomTypeFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReservationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    RoomCount = table.Column<int>(type: "int", nullable: false),
                    BuyPrice = table.Column<double>(type: "float", maxLength: 10, nullable: true),
                    SellPrice = table.Column<double>(type: "float", maxLength: 10, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationDetails", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_ReservationDetails_ReservationHotels_GidReservationHotelFK",
                        column: x => x.GidReservationHotelFK,
                        principalTable: "ReservationHotels",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservationDetails_RoomTypes_GidRoomTypeFK",
                        column: x => x.GidRoomTypeFK,
                        principalTable: "RoomTypes",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReservationRooms",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidReservationDetailFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomNo = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationRooms", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_ReservationRooms_ReservationDetails_GidReservationDetailFK",
                        column: x => x.GidReservationDetailFK,
                        principalTable: "ReservationDetails",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccommodationDates",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidReservationDetailFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidGuestFK = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GidRoomNoFK = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    PreviousRoomInfo = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccommodationDates", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_AccommodationDates_Guests_GidGuestFK",
                        column: x => x.GidGuestFK,
                        principalTable: "Guests",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccommodationDates_ReservationDetails_GidReservationDetailFK",
                        column: x => x.GidReservationDetailFK,
                        principalTable: "ReservationDetails",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccommodationDates_ReservationRooms_GidRoomNoFK",
                        column: x => x.GidRoomNoFK,
                        principalTable: "ReservationRooms",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationDates_GidGuestFK",
                table: "AccommodationDates",
                column: "GidGuestFK");

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationDates_GidReservationDetailFK",
                table: "AccommodationDates",
                column: "GidReservationDetailFK");

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationDates_GidRoomNoFK",
                table: "AccommodationDates",
                column: "GidRoomNoFK");

            migrationBuilder.CreateIndex(
                name: "IX_Guests_GidNationalityFK",
                table: "Guests",
                column: "GidNationalityFK");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationDetails_GidReservationHotelFK",
                table: "ReservationDetails",
                column: "GidReservationHotelFK");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationDetails_GidRoomTypeFK",
                table: "ReservationDetails",
                column: "GidRoomTypeFK");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHotels_GidBuyCurrencyTypeFK",
                table: "ReservationHotels",
                column: "GidBuyCurrencyTypeFK");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHotels_GidHotelFK",
                table: "ReservationHotels",
                column: "GidHotelFK");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHotels_GidReservationFK",
                table: "ReservationHotels",
                column: "GidReservationFK");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHotels_GidSellCurrencyTypeFK",
                table: "ReservationHotels",
                column: "GidSellCurrencyTypeFK");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHotelStaffs_GidHotelFK",
                table: "ReservationHotelStaffs",
                column: "GidHotelFK");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationRooms_GidReservationDetailFK",
                table: "ReservationRooms",
                column: "GidReservationDetailFK");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_GidOrganizationFK",
                table: "Reservations",
                column: "GidOrganizationFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccommodationDates");

            migrationBuilder.DropTable(
                name: "ReservationHotelStaffs");

            migrationBuilder.DropTable(
                name: "Guests");

            migrationBuilder.DropTable(
                name: "ReservationRooms");

            migrationBuilder.DropTable(
                name: "ReservationDetails");

            migrationBuilder.DropTable(
                name: "ReservationHotels");

            migrationBuilder.DropTable(
                name: "Reservations");
        }
    }
}
