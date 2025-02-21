using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_Users_GidUserFK",
                table: "Announcements");

            migrationBuilder.DropIndex(
                name: "IX_Announcements_GidUserFK",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "GidUserFK",
                table: "Announcements");

            migrationBuilder.AddColumn<Guid>(
                name: "GidClubFK",
                table: "Admins",
                type: "uniqueidentifier",
                nullable: true,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Admins_GidClubFK",
                table: "Admins",
                column: "GidClubFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_Clubs_GidClubFK",
                table: "Admins",
                column: "GidClubFK",
                principalTable: "Clubs",
                principalColumn: "Gid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_Clubs_GidClubFK",
                table: "Admins");

            migrationBuilder.DropIndex(
                name: "IX_Admins_GidClubFK",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "GidClubFK",
                table: "Admins");

            migrationBuilder.AddColumn<Guid>(
                name: "GidUserFK",
                table: "Announcements",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_GidUserFK",
                table: "Announcements",
                column: "GidUserFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_Users_GidUserFK",
                table: "Announcements",
                column: "GidUserFK",
                principalTable: "Users",
                principalColumn: "Gid",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
