using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mig15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GidVehicleAllFK",
                table: "VehicleTransaction",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "VehicleAllFKGid",
                table: "VehicleTransaction",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTransaction_VehicleAllFKGid",
                table: "VehicleTransaction",
                column: "VehicleAllFKGid");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleTransaction_VehicleAll_VehicleAllFKGid",
                table: "VehicleTransaction",
                column: "VehicleAllFKGid",
                principalTable: "VehicleAll",
                principalColumn: "Gid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleTransaction_VehicleAll_VehicleAllFKGid",
                table: "VehicleTransaction");

            migrationBuilder.DropIndex(
                name: "IX_VehicleTransaction_VehicleAllFKGid",
                table: "VehicleTransaction");

            migrationBuilder.DropColumn(
                name: "GidVehicleAllFK",
                table: "VehicleTransaction");

            migrationBuilder.DropColumn(
                name: "VehicleAllFKGid",
                table: "VehicleTransaction");
        }
    }
}
