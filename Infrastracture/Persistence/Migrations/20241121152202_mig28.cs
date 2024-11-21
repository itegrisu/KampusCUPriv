using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mig28 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GidFeeCurrencyFK",
                table: "Transportations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transportations_GidFeeCurrencyFK",
                table: "Transportations",
                column: "GidFeeCurrencyFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Transportations_Currencies_GidFeeCurrencyFK",
                table: "Transportations",
                column: "GidFeeCurrencyFK",
                principalTable: "Currencies",
                principalColumn: "Gid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transportations_Currencies_GidFeeCurrencyFK",
                table: "Transportations");

            migrationBuilder.DropIndex(
                name: "IX_Transportations_GidFeeCurrencyFK",
                table: "Transportations");

            migrationBuilder.DropColumn(
                name: "GidFeeCurrencyFK",
                table: "Transportations");
        }
    }
}
