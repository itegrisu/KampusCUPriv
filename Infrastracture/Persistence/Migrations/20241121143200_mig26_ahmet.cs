using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mig26_ahmet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GidFeeCurrencyFK",
                table: "VehicleTransactions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTransactions_GidFeeCurrencyFK",
                table: "VehicleTransactions",
                column: "GidFeeCurrencyFK");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleTransactions_Currencies_GidFeeCurrencyFK",
                table: "VehicleTransactions",
                column: "GidFeeCurrencyFK",
                principalTable: "Currencies",
                principalColumn: "Gid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleTransactions_Currencies_GidFeeCurrencyFK",
                table: "VehicleTransactions");

            migrationBuilder.DropIndex(
                name: "IX_VehicleTransactions_GidFeeCurrencyFK",
                table: "VehicleTransactions");

            migrationBuilder.DropColumn(
                name: "GidFeeCurrencyFK",
                table: "VehicleTransactions");
        }
    }
}
