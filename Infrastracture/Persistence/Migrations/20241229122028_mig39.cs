using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mig39 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GuestAccommodations",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidHotelFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidBuyCurrencyFK = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GidSellCurrencyFK = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Title = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Institution = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    GuestCount = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    GuestAccommodationStatus = table.Column<int>(type: "int", nullable: false),
                    BuyPrice = table.Column<double>(type: "float", maxLength: 10, nullable: true),
                    SellPrice = table.Column<double>(type: "float", maxLength: 10, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestAccommodations", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_GuestAccommodations_Currencies_GidBuyCurrencyFK",
                        column: x => x.GidBuyCurrencyFK,
                        principalTable: "Currencies",
                        principalColumn: "Gid");
                    table.ForeignKey(
                        name: "FK_GuestAccommodations_Currencies_GidSellCurrencyFK",
                        column: x => x.GidSellCurrencyFK,
                        principalTable: "Currencies",
                        principalColumn: "Gid");
                    table.ForeignKey(
                        name: "FK_GuestAccommodations_SCCompanies_GidHotelFK",
                        column: x => x.GidHotelFK,
                        principalTable: "SCCompanies",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GuestAccommodationPersons",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidGuestAccommodationFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidNationalityFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    FullName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Description = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestAccommodationPersons", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_GuestAccommodationPersons_Countries_GidNationalityFK",
                        column: x => x.GidNationalityFK,
                        principalTable: "Countries",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GuestAccommodationPersons_GuestAccommodations_GidGuestAccommodationFK",
                        column: x => x.GidGuestAccommodationFK,
                        principalTable: "GuestAccommodations",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GuestAccommodationRooms",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidGuestAccommodationFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidRoomTypeFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestAccommodationRooms", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_GuestAccommodationRooms_GuestAccommodations_GidGuestAccommodationFK",
                        column: x => x.GidGuestAccommodationFK,
                        principalTable: "GuestAccommodations",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GuestAccommodationRooms_RoomTypes_GidRoomTypeFK",
                        column: x => x.GidRoomTypeFK,
                        principalTable: "RoomTypes",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GuestAccommodationResults",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidGuestAccommodationPersonFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidGuestAccommodationRoomFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Note = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestAccommodationResults", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_GuestAccommodationResults_GuestAccommodationPersons_GidGuestAccommodationPersonFK",
                        column: x => x.GidGuestAccommodationPersonFK,
                        principalTable: "GuestAccommodationPersons",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GuestAccommodationResults_GuestAccommodationRooms_GidGuestAccommodationRoomFK",
                        column: x => x.GidGuestAccommodationRoomFK,
                        principalTable: "GuestAccommodationRooms",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuestAccommodationPersons_GidGuestAccommodationFK",
                table: "GuestAccommodationPersons",
                column: "GidGuestAccommodationFK");

            migrationBuilder.CreateIndex(
                name: "IX_GuestAccommodationPersons_GidNationalityFK",
                table: "GuestAccommodationPersons",
                column: "GidNationalityFK");

            migrationBuilder.CreateIndex(
                name: "IX_GuestAccommodationResults_GidGuestAccommodationPersonFK",
                table: "GuestAccommodationResults",
                column: "GidGuestAccommodationPersonFK");

            migrationBuilder.CreateIndex(
                name: "IX_GuestAccommodationResults_GidGuestAccommodationRoomFK",
                table: "GuestAccommodationResults",
                column: "GidGuestAccommodationRoomFK");

            migrationBuilder.CreateIndex(
                name: "IX_GuestAccommodationRooms_GidGuestAccommodationFK",
                table: "GuestAccommodationRooms",
                column: "GidGuestAccommodationFK");

            migrationBuilder.CreateIndex(
                name: "IX_GuestAccommodationRooms_GidRoomTypeFK",
                table: "GuestAccommodationRooms",
                column: "GidRoomTypeFK");

            migrationBuilder.CreateIndex(
                name: "IX_GuestAccommodations_GidBuyCurrencyFK",
                table: "GuestAccommodations",
                column: "GidBuyCurrencyFK");

            migrationBuilder.CreateIndex(
                name: "IX_GuestAccommodations_GidHotelFK",
                table: "GuestAccommodations",
                column: "GidHotelFK");

            migrationBuilder.CreateIndex(
                name: "IX_GuestAccommodations_GidSellCurrencyFK",
                table: "GuestAccommodations",
                column: "GidSellCurrencyFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuestAccommodationResults");

            migrationBuilder.DropTable(
                name: "GuestAccommodationPersons");

            migrationBuilder.DropTable(
                name: "GuestAccommodationRooms");

            migrationBuilder.DropTable(
                name: "GuestAccommodations");
        }
    }
}
