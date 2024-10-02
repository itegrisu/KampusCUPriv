using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "StockCards");

            migrationBuilder.RenameColumn(
                name: "WarehouseName",
                table: "Warehouses",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "TaxRate",
                table: "StockCards",
                newName: "StockType");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Warehouses",
                type: "varchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Warehouses",
                type: "varchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GidOrganizationFK",
                table: "Warehouses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WarehouseType",
                table: "Warehouses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Users",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "StockCode",
                table: "StockCards",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "StockCards",
                type: "varchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1500)",
                oldMaxLength: 1500,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "StockCards",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GidMeasureFK",
                table: "StockCards",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "GidStockCategoryFK",
                table: "StockCards",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "FinanceExpenseGroups",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    ExpenseGroupStatus = table.Column<int>(type: "int", nullable: false),
                    RowNo = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinanceExpenseGroups", x => x.Gid);
                });

            migrationBuilder.CreateTable(
                name: "FinanceIncomeGroups",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IncomeGroupName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    IncomeGroupStatus = table.Column<int>(type: "int", nullable: false),
                    RowNo = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinanceIncomeGroups", x => x.Gid);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    Customer = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    OfferStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Gid);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationTypes",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationTypes", x => x.Gid);
                });

            migrationBuilder.CreateTable(
                name: "StockCategories",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockCategories", x => x.Gid);
                });

            migrationBuilder.CreateTable(
                name: "FinanceIncomes",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidIncomeGroupFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidCurrencyFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Fee = table.Column<double>(type: "float", maxLength: 10, nullable: false),
                    MaturityDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IncomeStatus = table.Column<int>(type: "int", nullable: false),
                    Document = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Description = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinanceIncomes", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_FinanceIncomes_Currencies_GidCurrencyFK",
                        column: x => x.GidCurrencyFK,
                        principalTable: "Currencies",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinanceIncomes_FinanceIncomeGroups_GidIncomeGroupFK",
                        column: x => x.GidIncomeGroupFK,
                        principalTable: "FinanceIncomeGroups",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OfferFiles",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidOfferFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Document = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Description = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferFiles", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_OfferFiles_Offers_GidOfferFK",
                        column: x => x.GidOfferFK,
                        principalTable: "Offers",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OfferTransactions",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidOfferFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidCurrencyFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfferId = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Total = table.Column<double>(type: "float", maxLength: 10, nullable: false),
                    OfferDeadline = table.Column<DateTime>(type: "datetime", nullable: true),
                    Document = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Description = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferTransactions", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_OfferTransactions_Currencies_GidCurrencyFK",
                        column: x => x.GidCurrencyFK,
                        principalTable: "Currencies",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OfferTransactions_Offers_GidOfferFK",
                        column: x => x.GidOfferFK,
                        principalTable: "Offers",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidCustomerFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidResponsibleUserFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidOrganizationTypeFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    OrganizationStatus = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_Organizations_OrganizationTypes_GidOrganizationTypeFK",
                        column: x => x.GidOrganizationTypeFK,
                        principalTable: "OrganizationTypes",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Organizations_SCCompanies_GidCustomerFK",
                        column: x => x.GidCustomerFK,
                        principalTable: "SCCompanies",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Organizations_Users_GidResponsibleUserFK",
                        column: x => x.GidResponsibleUserFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FinanceExpenses",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidExpenseGroupFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidOrganizationFK = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GidMoneySenderPersonnelFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidMoneyReceivePersonnelFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidCurrencyFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidApprovalReceiverFK = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    AmountSpent = table.Column<double>(type: "float", maxLength: 10, nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ExpenseStatus = table.Column<int>(type: "int", nullable: false),
                    Document = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Description = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    ReceiverAcceptStatus = table.Column<int>(type: "int", nullable: false),
                    ReceiverAcceptDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ReceiverRejectDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ReceiverIpAddress = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinanceExpenses", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_FinanceExpenses_Currencies_GidCurrencyFK",
                        column: x => x.GidCurrencyFK,
                        principalTable: "Currencies",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinanceExpenses_FinanceExpenseGroups_GidExpenseGroupFK",
                        column: x => x.GidExpenseGroupFK,
                        principalTable: "FinanceExpenseGroups",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinanceExpenses_Organizations_GidOrganizationFK",
                        column: x => x.GidOrganizationFK,
                        principalTable: "Organizations",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinanceExpenses_Users_GidApprovalReceiverFK",
                        column: x => x.GidApprovalReceiverFK,
                        principalTable: "Users",
                        principalColumn: "Gid");
                    table.ForeignKey(
                        name: "FK_FinanceExpenses_Users_GidMoneyReceivePersonnelFK",
                        column: x => x.GidMoneyReceivePersonnelFK,
                        principalTable: "Users",
                        principalColumn: "Gid");
                    table.ForeignKey(
                        name: "FK_FinanceExpenses_Users_GidMoneySenderPersonnelFK",
                        column: x => x.GidMoneySenderPersonnelFK,
                        principalTable: "Users",
                        principalColumn: "Gid");
                });

            migrationBuilder.CreateTable(
                name: "OrganizationGroups",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidOrganizationFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    RowNo = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationGroups", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_OrganizationGroups_Organizations_GidOrganizationFK",
                        column: x => x.GidOrganizationFK,
                        principalTable: "Organizations",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FinanceExpenseDetails",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidExpenseFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidSpendPersonnelFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidCurrencyFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidControlPersonnelFK = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SpentTitle = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Fee = table.Column<double>(type: "float", maxLength: 10, nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Document = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Description = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    ControlDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ControlDescription = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinanceExpenseDetails", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_FinanceExpenseDetails_Currencies_GidCurrencyFK",
                        column: x => x.GidCurrencyFK,
                        principalTable: "Currencies",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinanceExpenseDetails_FinanceExpenses_GidExpenseFK",
                        column: x => x.GidExpenseFK,
                        principalTable: "FinanceExpenses",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinanceExpenseDetails_Users_GidControlPersonnelFK",
                        column: x => x.GidControlPersonnelFK,
                        principalTable: "Users",
                        principalColumn: "Gid");
                    table.ForeignKey(
                        name: "FK_FinanceExpenseDetails_Users_GidSpendPersonnelFK",
                        column: x => x.GidSpendPersonnelFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationItems",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidOrganizationGroupFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidMainResponsibleUserFK = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ItemName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Priority = table.Column<bool>(type: "bit", nullable: false),
                    IsStar = table.Column<bool>(type: "bit", nullable: false),
                    ItemStatus = table.Column<int>(type: "int", nullable: false),
                    RowNo = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationItems", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_OrganizationItems_OrganizationGroups_GidOrganizationGroupFK",
                        column: x => x.GidOrganizationGroupFK,
                        principalTable: "OrganizationGroups",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrganizationItems_Users_GidMainResponsibleUserFK",
                        column: x => x.GidMainResponsibleUserFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_GidOrganizationFK",
                table: "Warehouses",
                column: "GidOrganizationFK");

            migrationBuilder.CreateIndex(
                name: "IX_StockCards_GidMeasureFK",
                table: "StockCards",
                column: "GidMeasureFK");

            migrationBuilder.CreateIndex(
                name: "IX_StockCards_GidStockCategoryFK",
                table: "StockCards",
                column: "GidStockCategoryFK");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceExpenseDetails_GidControlPersonnelFK",
                table: "FinanceExpenseDetails",
                column: "GidControlPersonnelFK");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceExpenseDetails_GidCurrencyFK",
                table: "FinanceExpenseDetails",
                column: "GidCurrencyFK");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceExpenseDetails_GidExpenseFK",
                table: "FinanceExpenseDetails",
                column: "GidExpenseFK");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceExpenseDetails_GidSpendPersonnelFK",
                table: "FinanceExpenseDetails",
                column: "GidSpendPersonnelFK");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceExpenses_GidApprovalReceiverFK",
                table: "FinanceExpenses",
                column: "GidApprovalReceiverFK");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceExpenses_GidCurrencyFK",
                table: "FinanceExpenses",
                column: "GidCurrencyFK");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceExpenses_GidExpenseGroupFK",
                table: "FinanceExpenses",
                column: "GidExpenseGroupFK");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceExpenses_GidMoneyReceivePersonnelFK",
                table: "FinanceExpenses",
                column: "GidMoneyReceivePersonnelFK");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceExpenses_GidMoneySenderPersonnelFK",
                table: "FinanceExpenses",
                column: "GidMoneySenderPersonnelFK");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceExpenses_GidOrganizationFK",
                table: "FinanceExpenses",
                column: "GidOrganizationFK");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceIncomes_GidCurrencyFK",
                table: "FinanceIncomes",
                column: "GidCurrencyFK");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceIncomes_GidIncomeGroupFK",
                table: "FinanceIncomes",
                column: "GidIncomeGroupFK");

            migrationBuilder.CreateIndex(
                name: "IX_OfferFiles_GidOfferFK",
                table: "OfferFiles",
                column: "GidOfferFK");

            migrationBuilder.CreateIndex(
                name: "IX_OfferTransactions_GidCurrencyFK",
                table: "OfferTransactions",
                column: "GidCurrencyFK");

            migrationBuilder.CreateIndex(
                name: "IX_OfferTransactions_GidOfferFK",
                table: "OfferTransactions",
                column: "GidOfferFK");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationGroups_GidOrganizationFK",
                table: "OrganizationGroups",
                column: "GidOrganizationFK");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationItems_GidMainResponsibleUserFK",
                table: "OrganizationItems",
                column: "GidMainResponsibleUserFK");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationItems_GidOrganizationGroupFK",
                table: "OrganizationItems",
                column: "GidOrganizationGroupFK");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_GidCustomerFK",
                table: "Organizations",
                column: "GidCustomerFK");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_GidOrganizationTypeFK",
                table: "Organizations",
                column: "GidOrganizationTypeFK");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_GidResponsibleUserFK",
                table: "Organizations",
                column: "GidResponsibleUserFK");

            migrationBuilder.AddForeignKey(
                name: "FK_StockCards_MeasureTypes_GidMeasureFK",
                table: "StockCards",
                column: "GidMeasureFK",
                principalTable: "MeasureTypes",
                principalColumn: "Gid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StockCards_StockCategories_GidStockCategoryFK",
                table: "StockCards",
                column: "GidStockCategoryFK",
                principalTable: "StockCategories",
                principalColumn: "Gid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_Organizations_GidOrganizationFK",
                table: "Warehouses",
                column: "GidOrganizationFK",
                principalTable: "Organizations",
                principalColumn: "Gid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockCards_MeasureTypes_GidMeasureFK",
                table: "StockCards");

            migrationBuilder.DropForeignKey(
                name: "FK_StockCards_StockCategories_GidStockCategoryFK",
                table: "StockCards");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_Organizations_GidOrganizationFK",
                table: "Warehouses");

            migrationBuilder.DropTable(
                name: "FinanceExpenseDetails");

            migrationBuilder.DropTable(
                name: "FinanceIncomes");

            migrationBuilder.DropTable(
                name: "OfferFiles");

            migrationBuilder.DropTable(
                name: "OfferTransactions");

            migrationBuilder.DropTable(
                name: "OrganizationItems");

            migrationBuilder.DropTable(
                name: "StockCategories");

            migrationBuilder.DropTable(
                name: "FinanceExpenses");

            migrationBuilder.DropTable(
                name: "FinanceIncomeGroups");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "OrganizationGroups");

            migrationBuilder.DropTable(
                name: "FinanceExpenseGroups");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "OrganizationTypes");

            migrationBuilder.DropIndex(
                name: "IX_Warehouses_GidOrganizationFK",
                table: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_StockCards_GidMeasureFK",
                table: "StockCards");

            migrationBuilder.DropIndex(
                name: "IX_StockCards_GidStockCategoryFK",
                table: "StockCards");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "GidOrganizationFK",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "WarehouseType",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "StockCards");

            migrationBuilder.DropColumn(
                name: "GidMeasureFK",
                table: "StockCards");

            migrationBuilder.DropColumn(
                name: "GidStockCategoryFK",
                table: "StockCards");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Warehouses",
                newName: "WarehouseName");

            migrationBuilder.RenameColumn(
                name: "StockType",
                table: "StockCards",
                newName: "TaxRate");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Warehouses",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<string>(
                name: "StockCode",
                table: "StockCards",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "StockCards",
                type: "varchar(1500)",
                maxLength: 1500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "StockCards",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
