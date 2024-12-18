using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mig32 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VehicleAccidents",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidVehicleFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccidentDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Driver = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    AccidentFile = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    AccidentImageFile = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleAccidents", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_VehicleAccidents_VehicleAlls_GidVehicleFK",
                        column: x => x.GidVehicleFK,
                        principalTable: "VehicleAlls",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleAccidents_GidVehicleFK",
                table: "VehicleAccidents",
                column: "GidVehicleFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleAccidents");
        }
    }
}
