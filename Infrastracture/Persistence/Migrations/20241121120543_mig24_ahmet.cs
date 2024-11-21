using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mig24_ahmet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserReminder_Users_GidUserFK",
                table: "UserReminder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserReminder",
                table: "UserReminder");

            migrationBuilder.RenameTable(
                name: "UserReminder",
                newName: "UserReminders");

            migrationBuilder.RenameIndex(
                name: "IX_UserReminder_GidUserFK",
                table: "UserReminders",
                newName: "IX_UserReminders_GidUserFK");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "UserReminders",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Document",
                table: "UserReminders",
                type: "varchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "UserReminders",
                type: "varchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "UserReminders",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserReminders",
                table: "UserReminders",
                column: "Gid");

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidCityFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DistrictCode = table.Column<int>(type: "int", nullable: false),
                    DistrictName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_Districts_Cities_GidCityFK",
                        column: x => x.GidCityFK,
                        principalTable: "Cities",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransportationExternalServices",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidSupplierFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidOrganizationFK = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GidFeeCurrencyFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Fee = table.Column<double>(type: "float", maxLength: 10, nullable: false),
                    PlateNo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    ExternalVehicleType = table.Column<int>(type: "int", nullable: false),
                    PassengerCapacity = table.Column<int>(type: "int", nullable: true),
                    VehicleOfficer = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    VehiclePhone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    IsHasFile = table.Column<bool>(type: "bit", nullable: true),
                    ExternalServiceStatus = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportationExternalServices", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_TransportationExternalServices_Currencies_GidFeeCurrencyFK",
                        column: x => x.GidFeeCurrencyFK,
                        principalTable: "Currencies",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransportationExternalServices_Organizations_GidOrganizationFK",
                        column: x => x.GidOrganizationFK,
                        principalTable: "Organizations",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransportationExternalServices_SCCompanies_GidSupplierFK",
                        column: x => x.GidSupplierFK,
                        principalTable: "SCCompanies",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transportations",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidOrganizationFK = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomerInfo = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    TransportationNo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Fee = table.Column<double>(type: "float", maxLength: 10, nullable: false),
                    TransportationStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transportations", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_Transportations_Organizations_GidOrganizationFK",
                        column: x => x.GidOrganizationFK,
                        principalTable: "Organizations",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FinanceBalances",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidSupplierCustomerFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidVehicleTransactionFK = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GidTransportationFK = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GidTransportationExternalServiceFK = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GidFeeCurrencyFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BalanceType = table.Column<int>(type: "int", nullable: false),
                    BalanceResourceType = table.Column<int>(type: "int", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Fee = table.Column<double>(type: "float", maxLength: 10, nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    PaymentFile = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Description = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinanceBalances", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_FinanceBalances_Currencies_GidFeeCurrencyFK",
                        column: x => x.GidFeeCurrencyFK,
                        principalTable: "Currencies",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinanceBalances_SCCompanies_GidSupplierCustomerFK",
                        column: x => x.GidSupplierCustomerFK,
                        principalTable: "SCCompanies",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinanceBalances_TransportationExternalServices_GidTransportationExternalServiceFK",
                        column: x => x.GidTransportationExternalServiceFK,
                        principalTable: "TransportationExternalServices",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinanceBalances_Transportations_GidTransportationFK",
                        column: x => x.GidTransportationFK,
                        principalTable: "Transportations",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinanceBalances_VehicleTransactions_GidVehicleTransactionFK",
                        column: x => x.GidVehicleTransactionFK,
                        principalTable: "VehicleTransactions",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransportationServices",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidTransportationFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidVehicleFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceNo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    StartKM = table.Column<int>(type: "int", nullable: true),
                    EndKM = table.Column<int>(type: "int", nullable: true),
                    VehiclePhone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    TransportationServiceStatus = table.Column<int>(type: "int", nullable: false),
                    TransportationFile = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Description = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportationServices", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_TransportationServices_Transportations_GidTransportationFK",
                        column: x => x.GidTransportationFK,
                        principalTable: "Transportations",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransportationServices_VehicleAlls_GidVehicleFK",
                        column: x => x.GidVehicleFK,
                        principalTable: "VehicleAlls",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransportationGroups",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidTransportationServiceFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidStartCountryFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidStartCityFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidStartDistrictFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidEndCountryFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidEndCityFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidEndDistrictFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    TransportationFee = table.Column<double>(type: "float", maxLength: 10, nullable: false),
                    StartPlace = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    EndPlace = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportationGroups", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_TransportationGroups_Cities_GidEndCityFK",
                        column: x => x.GidEndCityFK,
                        principalTable: "Cities",
                        principalColumn: "Gid");
                    table.ForeignKey(
                        name: "FK_TransportationGroups_Cities_GidStartCityFK",
                        column: x => x.GidStartCityFK,
                        principalTable: "Cities",
                        principalColumn: "Gid");
                    table.ForeignKey(
                        name: "FK_TransportationGroups_Countries_GidEndCountryFK",
                        column: x => x.GidEndCountryFK,
                        principalTable: "Countries",
                        principalColumn: "Gid");
                    table.ForeignKey(
                        name: "FK_TransportationGroups_Countries_GidStartCountryFK",
                        column: x => x.GidStartCountryFK,
                        principalTable: "Countries",
                        principalColumn: "Gid");
                    table.ForeignKey(
                        name: "FK_TransportationGroups_Districts_GidEndDistrictFK",
                        column: x => x.GidEndDistrictFK,
                        principalTable: "Districts",
                        principalColumn: "Gid");
                    table.ForeignKey(
                        name: "FK_TransportationGroups_Districts_GidStartDistrictFK",
                        column: x => x.GidStartDistrictFK,
                        principalTable: "Districts",
                        principalColumn: "Gid");
                    table.ForeignKey(
                        name: "FK_TransportationGroups_TransportationServices_GidTransportationServiceFK",
                        column: x => x.GidTransportationServiceFK,
                        principalTable: "TransportationServices",
                        principalColumn: "Gid");
                });

            migrationBuilder.CreateTable(
                name: "TransportationPersonnels",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidTransportationServiceFK = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GidStaffPersonnelFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportationPersonnels", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_TransportationPersonnels_TransportationServices_GidTransportationServiceFK",
                        column: x => x.GidTransportationServiceFK,
                        principalTable: "TransportationServices",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransportationPersonnels_Users_GidStaffPersonnelFK",
                        column: x => x.GidStaffPersonnelFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransportationPassengers",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidTransportationGroupFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Country = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    IdentityNo = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    FirstName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    PassengerStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportationPassengers", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_TransportationPassengers_TransportationGroups_GidTransportationGroupFK",
                        column: x => x.GidTransportationGroupFK,
                        principalTable: "TransportationGroups",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Districts_GidCityFK",
                table: "Districts",
                column: "GidCityFK");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceBalances_GidFeeCurrencyFK",
                table: "FinanceBalances",
                column: "GidFeeCurrencyFK");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceBalances_GidSupplierCustomerFK",
                table: "FinanceBalances",
                column: "GidSupplierCustomerFK");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceBalances_GidTransportationExternalServiceFK",
                table: "FinanceBalances",
                column: "GidTransportationExternalServiceFK");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceBalances_GidTransportationFK",
                table: "FinanceBalances",
                column: "GidTransportationFK");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceBalances_GidVehicleTransactionFK",
                table: "FinanceBalances",
                column: "GidVehicleTransactionFK");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationExternalServices_GidFeeCurrencyFK",
                table: "TransportationExternalServices",
                column: "GidFeeCurrencyFK");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationExternalServices_GidOrganizationFK",
                table: "TransportationExternalServices",
                column: "GidOrganizationFK");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationExternalServices_GidSupplierFK",
                table: "TransportationExternalServices",
                column: "GidSupplierFK");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationGroups_GidEndCityFK",
                table: "TransportationGroups",
                column: "GidEndCityFK");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationGroups_GidEndCountryFK",
                table: "TransportationGroups",
                column: "GidEndCountryFK");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationGroups_GidEndDistrictFK",
                table: "TransportationGroups",
                column: "GidEndDistrictFK");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationGroups_GidStartCityFK",
                table: "TransportationGroups",
                column: "GidStartCityFK");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationGroups_GidStartCountryFK",
                table: "TransportationGroups",
                column: "GidStartCountryFK");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationGroups_GidStartDistrictFK",
                table: "TransportationGroups",
                column: "GidStartDistrictFK");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationGroups_GidTransportationServiceFK",
                table: "TransportationGroups",
                column: "GidTransportationServiceFK");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationPassengers_GidTransportationGroupFK",
                table: "TransportationPassengers",
                column: "GidTransportationGroupFK");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationPersonnels_GidStaffPersonnelFK",
                table: "TransportationPersonnels",
                column: "GidStaffPersonnelFK");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationPersonnels_GidTransportationServiceFK",
                table: "TransportationPersonnels",
                column: "GidTransportationServiceFK");

            migrationBuilder.CreateIndex(
                name: "IX_Transportations_GidOrganizationFK",
                table: "Transportations",
                column: "GidOrganizationFK");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationServices_GidTransportationFK",
                table: "TransportationServices",
                column: "GidTransportationFK");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationServices_GidVehicleFK",
                table: "TransportationServices",
                column: "GidVehicleFK");

            migrationBuilder.AddForeignKey(
                name: "FK_UserReminders_Users_GidUserFK",
                table: "UserReminders",
                column: "GidUserFK",
                principalTable: "Users",
                principalColumn: "Gid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserReminders_Users_GidUserFK",
                table: "UserReminders");

            migrationBuilder.DropTable(
                name: "FinanceBalances");

            migrationBuilder.DropTable(
                name: "TransportationPassengers");

            migrationBuilder.DropTable(
                name: "TransportationPersonnels");

            migrationBuilder.DropTable(
                name: "TransportationExternalServices");

            migrationBuilder.DropTable(
                name: "TransportationGroups");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "TransportationServices");

            migrationBuilder.DropTable(
                name: "Transportations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserReminders",
                table: "UserReminders");

            migrationBuilder.RenameTable(
                name: "UserReminders",
                newName: "UserReminder");

            migrationBuilder.RenameIndex(
                name: "IX_UserReminders_GidUserFK",
                table: "UserReminder",
                newName: "IX_UserReminder_GidUserFK");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "UserReminder",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Document",
                table: "UserReminder",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "UserReminder",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "UserReminder",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserReminder",
                table: "UserReminder",
                column: "Gid");

            migrationBuilder.AddForeignKey(
                name: "FK_UserReminder_Users_GidUserFK",
                table: "UserReminder",
                column: "GidUserFK",
                principalTable: "Users",
                principalColumn: "Gid",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
