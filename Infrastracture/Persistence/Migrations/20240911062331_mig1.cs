using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthPages",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PageName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    RedirectName = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    PhysicalFilePath = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    MenuLink = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    PathForAuthCheck = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    IsShowMenu = table.Column<bool>(type: "bit", nullable: false),
                    HelpFileName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    RowNo = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthPages", x => x.Gid);
                });

            migrationBuilder.CreateTable(
                name: "AuthRoles",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    RoleDescription = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    IconImage = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    RowNo = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthRoles", x => x.Gid);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    Code = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false),
                    PhoneCode = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Gid);
                });

            migrationBuilder.CreateTable(
                name: "LogFailedLogins",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    Password = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    IpAddress = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    Description = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogFailedLogins", x => x.Gid);
                });

            migrationBuilder.CreateTable(
                name: "AuthRolePages",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidRoleFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidPageFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowNo = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthRolePages", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_AuthRolePages_AuthPages_GidPageFK",
                        column: x => x.GidPageFK,
                        principalTable: "AuthPages",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuthRolePages_AuthRoles_GidRoleFK",
                        column: x => x.GidRoleFK,
                        principalTable: "AuthRoles",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidUyrukFK = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Adi = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Soyadi = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    EPosta = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Unvani = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true),
                    Sifre = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    SifreHash = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    SifreGuncellemeToken = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    TokenGecerlilikSuresi = table.Column<DateTime>(type: "datetime", nullable: true),
                    ProfilResmi = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    AktifHesapMi = table.Column<bool>(type: "bit", nullable: false),
                    SistemAdminMi = table.Column<bool>(type: "bit", nullable: false),
                    Gsm = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    DogumYeri = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    DogumTarihi = table.Column<DateTime>(type: "datetime", nullable: true),
                    KimlikNo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    PasaportNo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    SGKNo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    EhliyetNo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Not = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true),
                    MedeniDurumu = table.Column<int>(type: "int", nullable: true),
                    KanGrubu = table.Column<int>(type: "int", nullable: true),
                    Cinsiyet = table.Column<int>(type: "int", nullable: false),
                    EMailAktivasyonDurumu = table.Column<int>(type: "int", nullable: false),
                    SmsAktivasyonDurumu = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_Users_Countries_GidUyrukFK",
                        column: x => x.GidUyrukFK,
                        principalTable: "Countries",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuthUserRoles",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidUserFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidRoleFK = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GidPageFK = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RowNo = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthUserRoles", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_AuthUserRoles_AuthPages_GidPageFK",
                        column: x => x.GidPageFK,
                        principalTable: "AuthPages",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuthUserRoles_AuthRoles_GidRoleFK",
                        column: x => x.GidRoleFK,
                        principalTable: "AuthRoles",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuthUserRoles_Users_GidUserFK",
                        column: x => x.GidUserFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LogAuthorizationErrors",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidUserFK = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IpAddress = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    PageInfo = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    Operation = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    JSonData = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogAuthorizationErrors", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_LogAuthorizationErrors_Users_GidUserFK",
                        column: x => x.GidUserFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LogEmailSends",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidUserFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Content = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogEmailSends", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_LogEmailSends_Users_GidUserFK",
                        column: x => x.GidUserFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LogSuccessedLogins",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidUserFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IpAddress = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    SessionId = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    LogOutDate = table.Column<string>(type: "varchar(48)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogSuccessedLogins", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_LogSuccessedLogins_Users_GidUserFK",
                        column: x => x.GidUserFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LogUserPageVisitActions",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidUserFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IpAddress = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    PageInfo = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    Operation = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    JSonData = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogUserPageVisitActions", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_LogUserPageVisitActions_Users_GidUserFK",
                        column: x => x.GidUserFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LogUserPageVisits",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidUserFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IpAddress = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    PageInfo = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    SessionId = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogUserPageVisits", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_LogUserPageVisits_Users_GidUserFK",
                        column: x => x.GidUserFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRefreshTokens",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidUserFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRefreshTokens", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_UserRefreshTokens_Users_GidUserFK",
                        column: x => x.GidUserFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthRolePages_GidPageFK",
                table: "AuthRolePages",
                column: "GidPageFK");

            migrationBuilder.CreateIndex(
                name: "IX_AuthRolePages_GidRoleFK",
                table: "AuthRolePages",
                column: "GidRoleFK");

            migrationBuilder.CreateIndex(
                name: "IX_AuthUserRoles_GidPageFK",
                table: "AuthUserRoles",
                column: "GidPageFK");

            migrationBuilder.CreateIndex(
                name: "IX_AuthUserRoles_GidRoleFK",
                table: "AuthUserRoles",
                column: "GidRoleFK");

            migrationBuilder.CreateIndex(
                name: "IX_AuthUserRoles_GidUserFK",
                table: "AuthUserRoles",
                column: "GidUserFK");

            migrationBuilder.CreateIndex(
                name: "IX_LogAuthorizationErrors_GidUserFK",
                table: "LogAuthorizationErrors",
                column: "GidUserFK");

            migrationBuilder.CreateIndex(
                name: "IX_LogEmailSends_GidUserFK",
                table: "LogEmailSends",
                column: "GidUserFK");

            migrationBuilder.CreateIndex(
                name: "IX_LogSuccessedLogins_GidUserFK",
                table: "LogSuccessedLogins",
                column: "GidUserFK");

            migrationBuilder.CreateIndex(
                name: "IX_LogUserPageVisitActions_GidUserFK",
                table: "LogUserPageVisitActions",
                column: "GidUserFK");

            migrationBuilder.CreateIndex(
                name: "IX_LogUserPageVisits_GidUserFK",
                table: "LogUserPageVisits",
                column: "GidUserFK");

            migrationBuilder.CreateIndex(
                name: "IX_UserRefreshTokens_GidUserFK",
                table: "UserRefreshTokens",
                column: "GidUserFK");

            migrationBuilder.CreateIndex(
                name: "IX_Users_GidUyrukFK",
                table: "Users",
                column: "GidUyrukFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthRolePages");

            migrationBuilder.DropTable(
                name: "AuthUserRoles");

            migrationBuilder.DropTable(
                name: "LogAuthorizationErrors");

            migrationBuilder.DropTable(
                name: "LogEmailSends");

            migrationBuilder.DropTable(
                name: "LogFailedLogins");

            migrationBuilder.DropTable(
                name: "LogSuccessedLogins");

            migrationBuilder.DropTable(
                name: "LogUserPageVisitActions");

            migrationBuilder.DropTable(
                name: "LogUserPageVisits");

            migrationBuilder.DropTable(
                name: "UserRefreshTokens");

            migrationBuilder.DropTable(
                name: "AuthPages");

            migrationBuilder.DropTable(
                name: "AuthRoles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
