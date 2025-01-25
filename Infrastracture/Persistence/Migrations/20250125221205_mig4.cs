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
                name: "FK_Announcements_AnnouncementTypes_GidAnnouncementType",
                table: "Announcements");

            migrationBuilder.DropTable(
                name: "AnnouncementTypes");

            migrationBuilder.DropTable(
                name: "Calendars");

            migrationBuilder.DropIndex(
                name: "IX_Announcements_GidAnnouncementType",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "Announcements");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Clubs",
                type: "varchar(8)",
                maxLength: 8,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnnouncementType",
                table: "Announcements",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StudenAnnouncements",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidUserFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidAnnouncementFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudenAnnouncements", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_StudenAnnouncements_Announcements_GidAnnouncementFK",
                        column: x => x.GidAnnouncementFK,
                        principalTable: "Announcements",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudenAnnouncements_Users_GidUserFK",
                        column: x => x.GidUserFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudenAnnouncements_GidAnnouncementFK",
                table: "StudenAnnouncements",
                column: "GidAnnouncementFK");

            migrationBuilder.CreateIndex(
                name: "IX_StudenAnnouncements_GidUserFK",
                table: "StudenAnnouncements",
                column: "GidUserFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudenAnnouncements");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "AnnouncementType",
                table: "Announcements");

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "Announcements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "AnnouncementTypes",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementTypes", x => x.Gid);
                });

            migrationBuilder.CreateTable(
                name: "Calendars",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidEventFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendars", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_Calendars_Events_GidEventFK",
                        column: x => x.GidEventFK,
                        principalTable: "Events",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_GidAnnouncementType",
                table: "Announcements",
                column: "GidAnnouncementType");

            migrationBuilder.CreateIndex(
                name: "IX_Calendars_GidEventFK",
                table: "Calendars",
                column: "GidEventFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_AnnouncementTypes_GidAnnouncementType",
                table: "Announcements",
                column: "GidAnnouncementType",
                principalTable: "AnnouncementTypes",
                principalColumn: "Gid",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
