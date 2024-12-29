using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mig40 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PartTimeWorkers",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdentityNo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    FullName = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    UserName = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    IsLoginStatus = table.Column<bool>(type: "bit", nullable: false),
                    Gsm = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartTimeWorkers", x => x.Gid);
                });

            migrationBuilder.CreateTable(
                name: "PartTimeWorkerFiles",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidPartTimeWorkerFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    WorkerFile = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    ExpiredDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartTimeWorkerFiles", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_PartTimeWorkerFiles_PartTimeWorkers_GidPartTimeWorkerFK",
                        column: x => x.GidPartTimeWorkerFK,
                        principalTable: "PartTimeWorkers",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PartTimeWorkerForeignLanguages",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidPartTimeWorkerFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidForeignLanguageFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartTimeWorkerForeignLanguages", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_PartTimeWorkerForeignLanguages_ForeignLanguages_GidForeignLanguageFK",
                        column: x => x.GidForeignLanguageFK,
                        principalTable: "ForeignLanguages",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartTimeWorkerForeignLanguages_PartTimeWorkers_GidPartTimeWorkerFK",
                        column: x => x.GidPartTimeWorkerFK,
                        principalTable: "PartTimeWorkers",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReservationHotelPartTimeWorkers",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidHotelFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidPartTimeWorkerFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Note = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationHotelPartTimeWorkers", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_ReservationHotelPartTimeWorkers_PartTimeWorkers_GidPartTimeWorkerFK",
                        column: x => x.GidPartTimeWorkerFK,
                        principalTable: "PartTimeWorkers",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservationHotelPartTimeWorkers_ReservationHotels_GidHotelFK",
                        column: x => x.GidHotelFK,
                        principalTable: "ReservationHotels",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartTimeWorkerFiles_GidPartTimeWorkerFK",
                table: "PartTimeWorkerFiles",
                column: "GidPartTimeWorkerFK");

            migrationBuilder.CreateIndex(
                name: "IX_PartTimeWorkerForeignLanguages_GidForeignLanguageFK",
                table: "PartTimeWorkerForeignLanguages",
                column: "GidForeignLanguageFK");

            migrationBuilder.CreateIndex(
                name: "IX_PartTimeWorkerForeignLanguages_GidPartTimeWorkerFK",
                table: "PartTimeWorkerForeignLanguages",
                column: "GidPartTimeWorkerFK");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHotelPartTimeWorkers_GidHotelFK",
                table: "ReservationHotelPartTimeWorkers",
                column: "GidHotelFK");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHotelPartTimeWorkers_GidPartTimeWorkerFK",
                table: "ReservationHotelPartTimeWorkers",
                column: "GidPartTimeWorkerFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartTimeWorkerFiles");

            migrationBuilder.DropTable(
                name: "PartTimeWorkerForeignLanguages");

            migrationBuilder.DropTable(
                name: "ReservationHotelPartTimeWorkers");

            migrationBuilder.DropTable(
                name: "PartTimeWorkers");
        }
    }
}
