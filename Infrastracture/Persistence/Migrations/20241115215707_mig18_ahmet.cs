using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mig18_ahmet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleTransactions_VehicleAlls_GidVehicleAllFK",
                table: "VehicleTransactions");

            migrationBuilder.RenameColumn(
                name: "GidVehicleAllFK",
                table: "VehicleTransactions",
                newName: "GidVehicleFK");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleTransactions_GidVehicleAllFK",
                table: "VehicleTransactions",
                newName: "IX_VehicleTransactions_GidVehicleFK");

            migrationBuilder.CreateTable(
                name: "TyreTypes",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    TyreTypeName = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TyreTypes", x => x.Gid);
                });

            migrationBuilder.CreateTable(
                name: "VehicleDocuments",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidVehicleFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidDocumentType = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    DocumentDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    DocumentLastDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DocumentFile = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Description = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleDocuments", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_VehicleDocuments_DocumentTypes_GidDocumentType",
                        column: x => x.GidDocumentType,
                        principalTable: "DocumentTypes",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleDocuments_VehicleAlls_GidVehicleFK",
                        column: x => x.GidVehicleFK,
                        principalTable: "VehicleAlls",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VehicleEquipments",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidVehicleFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EquipmentName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    DocumentFile = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Description = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleEquipments", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_VehicleEquipments_VehicleAlls_GidVehicleFK",
                        column: x => x.GidVehicleFK,
                        principalTable: "VehicleAlls",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VehicleFuels",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidVehicleFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleFuels", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_VehicleFuels_VehicleAlls_GidVehicleFK",
                        column: x => x.GidVehicleFK,
                        principalTable: "VehicleAlls",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VehicleInspections",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidVehicleFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    DocumentFile = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Description = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleInspections", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_VehicleInspections_VehicleAlls_GidVehicleFK",
                        column: x => x.GidVehicleFK,
                        principalTable: "VehicleAlls",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VehicleInsurances",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidVehicleFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsuranceType = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    InsuranceFee = table.Column<int>(type: "int", nullable: false),
                    DocumentFile = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Description = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleInsurances", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_VehicleInsurances_VehicleAlls_GidVehicleFK",
                        column: x => x.GidVehicleFK,
                        principalTable: "VehicleAlls",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VehicleMaintenances",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidVehicleFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaintenanceDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ResponsiblePerson = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    MaintenanceFee = table.Column<int>(type: "int", nullable: false),
                    DocumentFile = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Description = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    MaintenanceScore = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleMaintenances", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_VehicleMaintenances_VehicleAlls_GidVehicleFK",
                        column: x => x.GidVehicleFK,
                        principalTable: "VehicleAlls",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tyres",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidTyreTypeFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TyreNo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    ProductionYear = table.Column<int>(type: "int", nullable: true),
                    DateOfPurchase = table.Column<DateTime>(type: "datetime", nullable: true),
                    TyreStatus = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tyres", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_Tyres_TyreTypes_GidTyreTypeFK",
                        column: x => x.GidTyreTypeFK,
                        principalTable: "TyreTypes",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tyres_GidTyreTypeFK",
                table: "Tyres",
                column: "GidTyreTypeFK");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleDocuments_GidDocumentType",
                table: "VehicleDocuments",
                column: "GidDocumentType");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleDocuments_GidVehicleFK",
                table: "VehicleDocuments",
                column: "GidVehicleFK");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleEquipments_GidVehicleFK",
                table: "VehicleEquipments",
                column: "GidVehicleFK");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleFuels_GidVehicleFK",
                table: "VehicleFuels",
                column: "GidVehicleFK");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleInspections_GidVehicleFK",
                table: "VehicleInspections",
                column: "GidVehicleFK");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleInsurances_GidVehicleFK",
                table: "VehicleInsurances",
                column: "GidVehicleFK");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleMaintenances_GidVehicleFK",
                table: "VehicleMaintenances",
                column: "GidVehicleFK");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleTransactions_VehicleAlls_GidVehicleFK",
                table: "VehicleTransactions",
                column: "GidVehicleFK",
                principalTable: "VehicleAlls",
                principalColumn: "Gid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleTransactions_VehicleAlls_GidVehicleFK",
                table: "VehicleTransactions");

            migrationBuilder.DropTable(
                name: "Tyres");

            migrationBuilder.DropTable(
                name: "VehicleDocuments");

            migrationBuilder.DropTable(
                name: "VehicleEquipments");

            migrationBuilder.DropTable(
                name: "VehicleFuels");

            migrationBuilder.DropTable(
                name: "VehicleInspections");

            migrationBuilder.DropTable(
                name: "VehicleInsurances");

            migrationBuilder.DropTable(
                name: "VehicleMaintenances");

            migrationBuilder.DropTable(
                name: "TyreTypes");

            migrationBuilder.RenameColumn(
                name: "GidVehicleFK",
                table: "VehicleTransactions",
                newName: "GidVehicleAllFK");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleTransactions_GidVehicleFK",
                table: "VehicleTransactions",
                newName: "IX_VehicleTransactions_GidVehicleAllFK");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleTransactions_VehicleAlls_GidVehicleAllFK",
                table: "VehicleTransactions",
                column: "GidVehicleAllFK",
                principalTable: "VehicleAlls",
                principalColumn: "Gid",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
