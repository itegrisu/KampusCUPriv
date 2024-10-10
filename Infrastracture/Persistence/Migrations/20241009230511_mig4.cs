using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MarketingCustomers",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Company = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Duty = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    PreviousDuty = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    Gsm = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketingCustomers", x => x.Gid);
                });

            migrationBuilder.CreateTable(
                name: "MarketingVisitPlans",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidPersonnelFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidVisitCustomerFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    PlanningVisitDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    VisitStatus = table.Column<int>(type: "int", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    VisitRank = table.Column<int>(type: "int", nullable: true),
                    VisitNote = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketingVisitPlans", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_MarketingVisitPlans_MarketingCustomers_GidVisitCustomerFK",
                        column: x => x.GidVisitCustomerFK,
                        principalTable: "MarketingCustomers",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MarketingVisitPlans_Users_GidPersonnelFK",
                        column: x => x.GidPersonnelFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MarketingVisitPlans_GidPersonnelFK",
                table: "MarketingVisitPlans",
                column: "GidPersonnelFK");

            migrationBuilder.CreateIndex(
                name: "IX_MarketingVisitPlans_GidVisitCustomerFK",
                table: "MarketingVisitPlans",
                column: "GidVisitCustomerFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarketingVisitPlans");

            migrationBuilder.DropTable(
                name: "MarketingCustomers");
        }
    }
}
