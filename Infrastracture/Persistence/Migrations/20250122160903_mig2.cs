using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentClubs",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidUserFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidClubFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentClubs", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_StudentClubs_Clubs_GidClubFK",
                        column: x => x.GidClubFK,
                        principalTable: "Clubs",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentClubs_Users_GidUserFK",
                        column: x => x.GidUserFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentClubs_GidClubFK",
                table: "StudentClubs",
                column: "GidClubFK");

            migrationBuilder.CreateIndex(
                name: "IX_StudentClubs_GidUserFK",
                table: "StudentClubs",
                column: "GidUserFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentClubs");
        }
    }
}
