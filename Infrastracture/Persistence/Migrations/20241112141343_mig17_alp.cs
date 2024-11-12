using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mig17_alp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrganizationItemFiles",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidOrganizationItemFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Document = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Description = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true),
                    RowNo = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationItemFiles", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_OrganizationItemFiles_OrganizationItems_GidOrganizationItemFK",
                        column: x => x.GidOrganizationItemFK,
                        principalTable: "OrganizationItems",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationItemMessages",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidOrganizationItemFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidSendMessageUserFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Message = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationItemMessages", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_OrganizationItemMessages_OrganizationItems_GidOrganizationItemFK",
                        column: x => x.GidOrganizationItemFK,
                        principalTable: "OrganizationItems",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrganizationItemMessages_Users_GidSendMessageUserFK",
                        column: x => x.GidSendMessageUserFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationItemFiles_GidOrganizationItemFK",
                table: "OrganizationItemFiles",
                column: "GidOrganizationItemFK");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationItemMessages_GidOrganizationItemFK",
                table: "OrganizationItemMessages",
                column: "GidOrganizationItemFK");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationItemMessages_GidSendMessageUserFK",
                table: "OrganizationItemMessages",
                column: "GidSendMessageUserFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrganizationItemFiles");

            migrationBuilder.DropTable(
                name: "OrganizationItemMessages");
        }
    }
}
