using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mig43 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerInfo",
                table: "Transportations");

            migrationBuilder.AddColumn<Guid>(
                name: "GidCustomerFK",
                table: "Transportations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transportations_GidCustomerFK",
                table: "Transportations",
                column: "GidCustomerFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Transportations_SCCompanies_GidCustomerFK",
                table: "Transportations",
                column: "GidCustomerFK",
                principalTable: "SCCompanies",
                principalColumn: "Gid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transportations_SCCompanies_GidCustomerFK",
                table: "Transportations");

            migrationBuilder.DropIndex(
                name: "IX_Transportations_GidCustomerFK",
                table: "Transportations");

            migrationBuilder.DropColumn(
                name: "GidCustomerFK",
                table: "Transportations");

            migrationBuilder.AddColumn<string>(
                name: "CustomerInfo",
                table: "Transportations",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }
    }
}
