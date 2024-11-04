using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mig10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SCPersonnel",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidSCCompanyFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidPersonnelFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SCPersonnelLoginStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SCPersonnel", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_SCPersonnel_SCCompanies_GidSCCompanyFK",
                        column: x => x.GidSCCompanyFK,
                        principalTable: "SCCompanies",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SCPersonnel_Users_GidPersonnelFK",
                        column: x => x.GidPersonnelFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SCPersonnel_GidPersonnelFK",
                table: "SCPersonnel",
                column: "GidPersonnelFK");

            migrationBuilder.CreateIndex(
                name: "IX_SCPersonnel_GidSCCompanyFK",
                table: "SCPersonnel",
                column: "GidSCCompanyFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SCPersonnel");
        }
    }
}
