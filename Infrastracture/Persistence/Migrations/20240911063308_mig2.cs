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
                name: "PortalParameters",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    ParameterValueType = table.Column<int>(type: "int", nullable: false),
                    StringValue = table.Column<string>(type: "varchar(max)", nullable: true),
                    IntegerValue = table.Column<int>(type: "int", nullable: true),
                    DecimalValue = table.Column<double>(type: "float", nullable: true),
                    DateTimeValue = table.Column<DateTime>(type: "datetime", nullable: true),
                    Description = table.Column<string>(type: "varchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortalParameters", x => x.Gid);
                });

            migrationBuilder.CreateTable(
                name: "PortalTexts",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Content = table.Column<string>(type: "varchar(max)", nullable: true),
                    Description = table.Column<string>(type: "varchar(max)", nullable: true),
                    IsRichTextBox = table.Column<bool>(type: "bit", nullable: false),
                    ContentRich = table.Column<string>(type: "varchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortalTexts", x => x.Gid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PortalParameters");

            migrationBuilder.DropTable(
                name: "PortalTexts");
        }
    }
}
