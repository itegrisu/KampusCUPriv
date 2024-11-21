using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mig25_ahmet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransportationGroups_Cities_GidEndCityFK",
                table: "TransportationGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_TransportationGroups_Cities_GidStartCityFK",
                table: "TransportationGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_TransportationGroups_Countries_GidEndCountryFK",
                table: "TransportationGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_TransportationGroups_Countries_GidStartCountryFK",
                table: "TransportationGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_TransportationGroups_Districts_GidEndDistrictFK",
                table: "TransportationGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_TransportationGroups_Districts_GidStartDistrictFK",
                table: "TransportationGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_TransportationGroups_TransportationServices_GidTransportationServiceFK",
                table: "TransportationGroups");

            migrationBuilder.AddForeignKey(
                name: "FK_TransportationGroups_Cities_GidEndCityFK",
                table: "TransportationGroups",
                column: "GidEndCityFK",
                principalTable: "Cities",
                principalColumn: "Gid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransportationGroups_Cities_GidStartCityFK",
                table: "TransportationGroups",
                column: "GidStartCityFK",
                principalTable: "Cities",
                principalColumn: "Gid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransportationGroups_Countries_GidEndCountryFK",
                table: "TransportationGroups",
                column: "GidEndCountryFK",
                principalTable: "Countries",
                principalColumn: "Gid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransportationGroups_Countries_GidStartCountryFK",
                table: "TransportationGroups",
                column: "GidStartCountryFK",
                principalTable: "Countries",
                principalColumn: "Gid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransportationGroups_Districts_GidEndDistrictFK",
                table: "TransportationGroups",
                column: "GidEndDistrictFK",
                principalTable: "Districts",
                principalColumn: "Gid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransportationGroups_Districts_GidStartDistrictFK",
                table: "TransportationGroups",
                column: "GidStartDistrictFK",
                principalTable: "Districts",
                principalColumn: "Gid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransportationGroups_TransportationServices_GidTransportationServiceFK",
                table: "TransportationGroups",
                column: "GidTransportationServiceFK",
                principalTable: "TransportationServices",
                principalColumn: "Gid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransportationGroups_Cities_GidEndCityFK",
                table: "TransportationGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_TransportationGroups_Cities_GidStartCityFK",
                table: "TransportationGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_TransportationGroups_Countries_GidEndCountryFK",
                table: "TransportationGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_TransportationGroups_Countries_GidStartCountryFK",
                table: "TransportationGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_TransportationGroups_Districts_GidEndDistrictFK",
                table: "TransportationGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_TransportationGroups_Districts_GidStartDistrictFK",
                table: "TransportationGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_TransportationGroups_TransportationServices_GidTransportationServiceFK",
                table: "TransportationGroups");

            migrationBuilder.AddForeignKey(
                name: "FK_TransportationGroups_Cities_GidEndCityFK",
                table: "TransportationGroups",
                column: "GidEndCityFK",
                principalTable: "Cities",
                principalColumn: "Gid");

            migrationBuilder.AddForeignKey(
                name: "FK_TransportationGroups_Cities_GidStartCityFK",
                table: "TransportationGroups",
                column: "GidStartCityFK",
                principalTable: "Cities",
                principalColumn: "Gid");

            migrationBuilder.AddForeignKey(
                name: "FK_TransportationGroups_Countries_GidEndCountryFK",
                table: "TransportationGroups",
                column: "GidEndCountryFK",
                principalTable: "Countries",
                principalColumn: "Gid");

            migrationBuilder.AddForeignKey(
                name: "FK_TransportationGroups_Countries_GidStartCountryFK",
                table: "TransportationGroups",
                column: "GidStartCountryFK",
                principalTable: "Countries",
                principalColumn: "Gid");

            migrationBuilder.AddForeignKey(
                name: "FK_TransportationGroups_Districts_GidEndDistrictFK",
                table: "TransportationGroups",
                column: "GidEndDistrictFK",
                principalTable: "Districts",
                principalColumn: "Gid");

            migrationBuilder.AddForeignKey(
                name: "FK_TransportationGroups_Districts_GidStartDistrictFK",
                table: "TransportationGroups",
                column: "GidStartDistrictFK",
                principalTable: "Districts",
                principalColumn: "Gid");

            migrationBuilder.AddForeignKey(
                name: "FK_TransportationGroups_TransportationServices_GidTransportationServiceFK",
                table: "TransportationGroups",
                column: "GidTransportationServiceFK",
                principalTable: "TransportationServices",
                principalColumn: "Gid");
        }
    }
}
