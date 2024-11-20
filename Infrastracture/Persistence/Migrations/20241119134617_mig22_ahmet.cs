using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mig22_ahmet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VehicleRequests",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidVehicleFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidRequestUserFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidApprovedUserFK = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UseAim = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    VehicleApprovedStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleRequests", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_VehicleRequests_Users_GidApprovedUserFK",
                        column: x => x.GidApprovedUserFK,
                        principalTable: "Users",
                        principalColumn: "Gid");
                    table.ForeignKey(
                        name: "FK_VehicleRequests_Users_GidRequestUserFK",
                        column: x => x.GidRequestUserFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleRequests_VehicleAlls_GidVehicleFK",
                        column: x => x.GidVehicleFK,
                        principalTable: "VehicleAlls",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleRequests_GidApprovedUserFK",
                table: "VehicleRequests",
                column: "GidApprovedUserFK");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleRequests_GidRequestUserFK",
                table: "VehicleRequests",
                column: "GidRequestUserFK");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleRequests_GidVehicleFK",
                table: "VehicleRequests",
                column: "GidVehicleFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleRequests");
        }
    }
}
