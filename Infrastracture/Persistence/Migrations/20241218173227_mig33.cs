using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mig33 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleAccidents_VehicleAlls_GidVehicleFK",
                table: "VehicleAccidents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleAccidents",
                table: "VehicleAccidents");

            migrationBuilder.RenameTable(
                name: "VehicleAccidents",
                newName: "VehicleAccident");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleAccidents_GidVehicleFK",
                table: "VehicleAccident",
                newName: "IX_VehicleAccident_GidVehicleFK");

            migrationBuilder.AlterColumn<string>(
                name: "Driver",
                table: "VehicleAccident",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "AccidentImageFile",
                table: "VehicleAccident",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccidentFile",
                table: "VehicleAccident",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AccidentDate",
                table: "VehicleAccident",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleAccident",
                table: "VehicleAccident",
                column: "Gid");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleAccident_VehicleAlls_GidVehicleFK",
                table: "VehicleAccident",
                column: "GidVehicleFK",
                principalTable: "VehicleAlls",
                principalColumn: "Gid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleAccident_VehicleAlls_GidVehicleFK",
                table: "VehicleAccident");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleAccident",
                table: "VehicleAccident");

            migrationBuilder.RenameTable(
                name: "VehicleAccident",
                newName: "VehicleAccidents");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleAccident_GidVehicleFK",
                table: "VehicleAccidents",
                newName: "IX_VehicleAccidents_GidVehicleFK");

            migrationBuilder.AlterColumn<string>(
                name: "Driver",
                table: "VehicleAccidents",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "AccidentImageFile",
                table: "VehicleAccidents",
                type: "varchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccidentFile",
                table: "VehicleAccidents",
                type: "varchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AccidentDate",
                table: "VehicleAccidents",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleAccidents",
                table: "VehicleAccidents",
                column: "Gid");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleAccidents_VehicleAlls_GidVehicleFK",
                table: "VehicleAccidents",
                column: "GidVehicleFK",
                principalTable: "VehicleAlls",
                principalColumn: "Gid",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
