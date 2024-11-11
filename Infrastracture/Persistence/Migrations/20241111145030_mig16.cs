using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mig16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleAll_OtoBrands_GidVehicleBrand",
                table: "VehicleAll");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleTransaction_SCCompanies_GidSupplierCustomerFK",
                table: "VehicleTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleTransaction_Users_GidVehicleUsePersonnelFK",
                table: "VehicleTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleTransaction_VehicleAll_VehicleAllFKGid",
                table: "VehicleTransaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleTransaction",
                table: "VehicleTransaction");

            migrationBuilder.DropIndex(
                name: "IX_VehicleTransaction_VehicleAllFKGid",
                table: "VehicleTransaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleAll",
                table: "VehicleAll");

            migrationBuilder.DropColumn(
                name: "VehicleAllFKGid",
                table: "VehicleTransaction");

            migrationBuilder.RenameTable(
                name: "VehicleTransaction",
                newName: "VehicleTransactions");

            migrationBuilder.RenameTable(
                name: "VehicleAll",
                newName: "VehicleAlls");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleTransaction_GidVehicleUsePersonnelFK",
                table: "VehicleTransactions",
                newName: "IX_VehicleTransactions_GidVehicleUsePersonnelFK");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleTransaction_GidSupplierCustomerFK",
                table: "VehicleTransactions",
                newName: "IX_VehicleTransactions_GidSupplierCustomerFK");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleAll_GidVehicleBrand",
                table: "VehicleAlls",
                newName: "IX_VehicleAlls_GidVehicleBrand");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SaleDate",
                table: "VehicleTransactions",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PurchaseDate",
                table: "VehicleTransactions",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LicenseFile",
                table: "VehicleTransactions",
                type: "varchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "VehicleTransactions",
                type: "varchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ContractStartDate",
                table: "VehicleTransactions",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContractFile",
                table: "VehicleTransactions",
                type: "varchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ContractEndDate",
                table: "VehicleTransactions",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContactPhone",
                table: "VehicleTransactions",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContactPerson",
                table: "VehicleTransactions",
                type: "varchar(60)",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ArventoAPIInfo",
                table: "VehicleTransactions",
                type: "varchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PlateNumber",
                table: "VehicleAlls",
                type: "varchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "VehicleAlls",
                type: "varchar(60)",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EngineNo",
                table: "VehicleAlls",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "VehicleAlls",
                type: "varchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "VehicleAlls",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ChassisNumber",
                table: "VehicleAlls",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleTransactions",
                table: "VehicleTransactions",
                column: "Gid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleAlls",
                table: "VehicleAlls",
                column: "Gid");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTransactions_GidVehicleAllFK",
                table: "VehicleTransactions",
                column: "GidVehicleAllFK");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleAlls_OtoBrands_GidVehicleBrand",
                table: "VehicleAlls",
                column: "GidVehicleBrand",
                principalTable: "OtoBrands",
                principalColumn: "Gid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleTransactions_SCCompanies_GidSupplierCustomerFK",
                table: "VehicleTransactions",
                column: "GidSupplierCustomerFK",
                principalTable: "SCCompanies",
                principalColumn: "Gid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleTransactions_Users_GidVehicleUsePersonnelFK",
                table: "VehicleTransactions",
                column: "GidVehicleUsePersonnelFK",
                principalTable: "Users",
                principalColumn: "Gid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleTransactions_VehicleAlls_GidVehicleAllFK",
                table: "VehicleTransactions",
                column: "GidVehicleAllFK",
                principalTable: "VehicleAlls",
                principalColumn: "Gid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleAlls_OtoBrands_GidVehicleBrand",
                table: "VehicleAlls");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleTransactions_SCCompanies_GidSupplierCustomerFK",
                table: "VehicleTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleTransactions_Users_GidVehicleUsePersonnelFK",
                table: "VehicleTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleTransactions_VehicleAlls_GidVehicleAllFK",
                table: "VehicleTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleTransactions",
                table: "VehicleTransactions");

            migrationBuilder.DropIndex(
                name: "IX_VehicleTransactions_GidVehicleAllFK",
                table: "VehicleTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleAlls",
                table: "VehicleAlls");

            migrationBuilder.RenameTable(
                name: "VehicleTransactions",
                newName: "VehicleTransaction");

            migrationBuilder.RenameTable(
                name: "VehicleAlls",
                newName: "VehicleAll");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleTransactions_GidVehicleUsePersonnelFK",
                table: "VehicleTransaction",
                newName: "IX_VehicleTransaction_GidVehicleUsePersonnelFK");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleTransactions_GidSupplierCustomerFK",
                table: "VehicleTransaction",
                newName: "IX_VehicleTransaction_GidSupplierCustomerFK");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleAlls_GidVehicleBrand",
                table: "VehicleAll",
                newName: "IX_VehicleAll_GidVehicleBrand");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SaleDate",
                table: "VehicleTransaction",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PurchaseDate",
                table: "VehicleTransaction",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LicenseFile",
                table: "VehicleTransaction",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "VehicleTransaction",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ContractStartDate",
                table: "VehicleTransaction",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContractFile",
                table: "VehicleTransaction",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ContractEndDate",
                table: "VehicleTransaction",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContactPhone",
                table: "VehicleTransaction",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContactPerson",
                table: "VehicleTransaction",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ArventoAPIInfo",
                table: "VehicleTransaction",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VehicleAllFKGid",
                table: "VehicleTransaction",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "PlateNumber",
                table: "VehicleAll",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "VehicleAll",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EngineNo",
                table: "VehicleAll",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "VehicleAll",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "VehicleAll",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ChassisNumber",
                table: "VehicleAll",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleTransaction",
                table: "VehicleTransaction",
                column: "Gid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleAll",
                table: "VehicleAll",
                column: "Gid");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTransaction_VehicleAllFKGid",
                table: "VehicleTransaction",
                column: "VehicleAllFKGid");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleAll_OtoBrands_GidVehicleBrand",
                table: "VehicleAll",
                column: "GidVehicleBrand",
                principalTable: "OtoBrands",
                principalColumn: "Gid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleTransaction_SCCompanies_GidSupplierCustomerFK",
                table: "VehicleTransaction",
                column: "GidSupplierCustomerFK",
                principalTable: "SCCompanies",
                principalColumn: "Gid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleTransaction_Users_GidVehicleUsePersonnelFK",
                table: "VehicleTransaction",
                column: "GidVehicleUsePersonnelFK",
                principalTable: "Users",
                principalColumn: "Gid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleTransaction_VehicleAll_VehicleAllFKGid",
                table: "VehicleTransaction",
                column: "VehicleAllFKGid",
                principalTable: "VehicleAll",
                principalColumn: "Gid",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
