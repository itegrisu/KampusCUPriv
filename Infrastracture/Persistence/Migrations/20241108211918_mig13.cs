using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mig13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VehicleTransaction",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidSupplierCustomerFK = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GidVehicleUsePersonnelFK = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StartKM = table.Column<int>(type: "int", nullable: false),
                    MonthlyRentalFee = table.Column<int>(type: "int", nullable: true),
                    ContractStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContractEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContactPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArventoAPIInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LicenseFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SaleDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VehicleStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTransaction", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_VehicleTransaction_SCCompanies_GidSupplierCustomerFK",
                        column: x => x.GidSupplierCustomerFK,
                        principalTable: "SCCompanies",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleTransaction_Users_GidVehicleUsePersonnelFK",
                        column: x => x.GidVehicleUsePersonnelFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTransaction_GidSupplierCustomerFK",
                table: "VehicleTransaction",
                column: "GidSupplierCustomerFK");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTransaction_GidVehicleUsePersonnelFK",
                table: "VehicleTransaction",
                column: "GidVehicleUsePersonnelFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleTransaction");
        }
    }
}
