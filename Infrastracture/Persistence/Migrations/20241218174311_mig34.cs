using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mig34 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tyres_TyreTypes_GidTyreTypeFK",
                table: "Tyres");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleTyreUses_Tyres_GidTyreFK",
                table: "VehicleTyreUses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tyres",
                table: "Tyres");

            migrationBuilder.RenameTable(
                name: "Tyres",
                newName: "VehicleTyres");

            migrationBuilder.RenameIndex(
                name: "IX_Tyres_GidTyreTypeFK",
                table: "VehicleTyres",
                newName: "IX_VehicleTyres_GidTyreTypeFK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleTyres",
                table: "VehicleTyres",
                column: "Gid");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleTyres_TyreTypes_GidTyreTypeFK",
                table: "VehicleTyres",
                column: "GidTyreTypeFK",
                principalTable: "TyreTypes",
                principalColumn: "Gid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleTyreUses_VehicleTyres_GidTyreFK",
                table: "VehicleTyreUses",
                column: "GidTyreFK",
                principalTable: "VehicleTyres",
                principalColumn: "Gid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleTyres_TyreTypes_GidTyreTypeFK",
                table: "VehicleTyres");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleTyreUses_VehicleTyres_GidTyreFK",
                table: "VehicleTyreUses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleTyres",
                table: "VehicleTyres");

            migrationBuilder.RenameTable(
                name: "VehicleTyres",
                newName: "Tyres");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleTyres_GidTyreTypeFK",
                table: "Tyres",
                newName: "IX_Tyres_GidTyreTypeFK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tyres",
                table: "Tyres",
                column: "Gid");

            migrationBuilder.AddForeignKey(
                name: "FK_Tyres_TyreTypes_GidTyreTypeFK",
                table: "Tyres",
                column: "GidTyreTypeFK",
                principalTable: "TyreTypes",
                principalColumn: "Gid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleTyreUses_Tyres_GidTyreFK",
                table: "VehicleTyreUses",
                column: "GidTyreFK",
                principalTable: "Tyres",
                principalColumn: "Gid",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
