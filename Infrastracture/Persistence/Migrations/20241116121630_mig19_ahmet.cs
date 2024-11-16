using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mig19_ahmet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VehicleTyreUses",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidVehicleFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidTyreFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InstallationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TyreRemovalDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTyreUses", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_VehicleTyreUses_Tyres_GidTyreFK",
                        column: x => x.GidTyreFK,
                        principalTable: "Tyres",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleTyreUses_VehicleAlls_GidVehicleFK",
                        column: x => x.GidVehicleFK,
                        principalTable: "VehicleAlls",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTyreUses_GidTyreFK",
                table: "VehicleTyreUses",
                column: "GidTyreFK");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTyreUses_GidVehicleFK",
                table: "VehicleTyreUses",
                column: "GidVehicleFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleTyreUses");
        }
    }
}
