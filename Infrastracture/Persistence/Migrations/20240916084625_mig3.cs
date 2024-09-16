using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Countries");

            migrationBuilder.RenameColumn(
                name: "PhoneCode",
                table: "Countries",
                newName: "UlkeKodu");

            migrationBuilder.AddColumn<int>(
                name: "RowNo",
                table: "Countries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TelefonKodu",
                table: "Countries",
                type: "varchar(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UlkeAdi",
                table: "Countries",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidUlkeFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SehirAdi = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    PlakaKodu = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_GidUlkeFK",
                        column: x => x.GidUlkeFK,
                        principalTable: "Countries",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DovizAdi = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    DovizKodu = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    DovizSimgesi = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Gid);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidAsilYoneticiFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidYedekYoneticiFK = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DepartmanAdi = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Detay = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_Departments_Users_GidAsilYoneticiFK",
                        column: x => x.GidAsilYoneticiFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Departments_Users_GidYedekYoneticiFK",
                        column: x => x.GidYedekYoneticiFK,
                        principalTable: "Users",
                        principalColumn: "Gid");
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BelgeAdi = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.Gid);
                });

            migrationBuilder.CreateTable(
                name: "ForeignLanguages",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DilAdi = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    DilKodu = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForeignLanguages", x => x.Gid);
                });

            migrationBuilder.CreateTable(
                name: "JobTypes",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GorevAdi = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTypes", x => x.Gid);
                });

            migrationBuilder.CreateTable(
                name: "MeasureTypes",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OlcuAdi = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasureTypes", x => x.Gid);
                });

            migrationBuilder.CreateTable(
                name: "OtoBrands",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AracMarkaAdi = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtoBrands", x => x.Gid);
                });

            migrationBuilder.CreateTable(
                name: "PermitTypes",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IzinAdi = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermitTypes", x => x.Gid);
                });

            migrationBuilder.CreateTable(
                name: "PersonnelGraduatedSchools",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidPersonelFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EgitimKurumuTuru = table.Column<int>(type: "int", nullable: false),
                    OkulBilgisi = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    BolumBilgisi = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    BaslamaYili = table.Column<int>(type: "int", nullable: false),
                    MezuniyetTarihi = table.Column<DateTime>(type: "datetime", nullable: true),
                    Belge = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Aciklama = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelGraduatedSchools", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_PersonnelGraduatedSchools_Users_GidPersonelFK",
                        column: x => x.GidPersonelFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonnelPassportInfos",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidPersonelFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PasaportNo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    VerilisTarihi = table.Column<DateTime>(type: "datetime", nullable: false),
                    GecerlilikTarihi = table.Column<DateTime>(type: "datetime", nullable: false),
                    Belge = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Aciklama = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelPassportInfos", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_PersonnelPassportInfos_Users_GidPersonelFK",
                        column: x => x.GidPersonelFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonnelResidenceInfos",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidPersonelFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OturumSeriNo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    VerilisTarihi = table.Column<DateTime>(type: "datetime", nullable: false),
                    GecerlilikTarihi = table.Column<DateTime>(type: "datetime", nullable: false),
                    Belge = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Aciklama = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelResidenceInfos", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_PersonnelResidenceInfos_Users_GidPersonelFK",
                        column: x => x.GidPersonelFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonnelWorkingTables",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidPersonelFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IseBaslamaTarihi = table.Column<DateTime>(type: "datetime", nullable: false),
                    IstenCikisTarihi = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelWorkingTables", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_PersonnelWorkingTables_Users_GidPersonelFK",
                        column: x => x.GidPersonelFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoomTypes",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OdaTuru = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    OdaKodu = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    KisiSayisi = table.Column<int>(type: "int", nullable: false),
                    Aciklama = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypes", x => x.Gid);
                });

            migrationBuilder.CreateTable(
                name: "PersonnelAddresses",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidPersonelFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidSehirFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdresBasligi = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Adres = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    Aciklama = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelAddresses", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_PersonnelAddresses_Cities_GidSehirFK",
                        column: x => x.GidSehirFK,
                        principalTable: "Cities",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonnelAddresses_Users_GidPersonelFK",
                        column: x => x.GidPersonelFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentUsers",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidDepartmanFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidPersonelFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentUsers", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_DepartmentUsers_Departments_GidDepartmanFK",
                        column: x => x.GidDepartmanFK,
                        principalTable: "Departments",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepartmentUsers_Users_GidPersonelFK",
                        column: x => x.GidPersonelFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonnelDocuments",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidPersonelFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidBelgeTuru = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BelgeAdi = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    GecerlilikTarihi = table.Column<DateTime>(type: "datetime", nullable: true),
                    Belge = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Aciklama = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelDocuments", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_PersonnelDocuments_DocumentTypes_GidBelgeTuru",
                        column: x => x.GidBelgeTuru,
                        principalTable: "DocumentTypes",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonnelDocuments_Users_GidPersonelFK",
                        column: x => x.GidPersonelFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonnelForeignLanguages",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidPersonelFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidLanguageFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KonusmaDuzeyi = table.Column<int>(type: "int", nullable: false),
                    OkumaDuzeyi = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelForeignLanguages", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_PersonnelForeignLanguages_ForeignLanguages_GidLanguageFK",
                        column: x => x.GidLanguageFK,
                        principalTable: "ForeignLanguages",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonnelForeignLanguages_Users_GidPersonelFK",
                        column: x => x.GidPersonelFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonnelPermitInfos",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidPersonelFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidPermitFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IzinBaslamaTarihi = table.Column<DateTime>(type: "datetime", nullable: false),
                    IzinBitisTarihi = table.Column<DateTime>(type: "datetime", nullable: false),
                    Belge = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Aciklama = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelPermitInfos", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_PersonnelPermitInfos_PermitTypes_GidPermitFK",
                        column: x => x.GidPermitFK,
                        principalTable: "PermitTypes",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonnelPermitInfos_Users_GidPersonelFK",
                        column: x => x.GidPersonelFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_GidUlkeFK",
                table: "Cities",
                column: "GidUlkeFK");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_GidAsilYoneticiFK",
                table: "Departments",
                column: "GidAsilYoneticiFK");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_GidYedekYoneticiFK",
                table: "Departments",
                column: "GidYedekYoneticiFK");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentUsers_GidDepartmanFK",
                table: "DepartmentUsers",
                column: "GidDepartmanFK");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentUsers_GidPersonelFK",
                table: "DepartmentUsers",
                column: "GidPersonelFK");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelAddresses_GidPersonelFK",
                table: "PersonnelAddresses",
                column: "GidPersonelFK");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelAddresses_GidSehirFK",
                table: "PersonnelAddresses",
                column: "GidSehirFK");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelDocuments_GidBelgeTuru",
                table: "PersonnelDocuments",
                column: "GidBelgeTuru");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelDocuments_GidPersonelFK",
                table: "PersonnelDocuments",
                column: "GidPersonelFK");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelForeignLanguages_GidLanguageFK",
                table: "PersonnelForeignLanguages",
                column: "GidLanguageFK");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelForeignLanguages_GidPersonelFK",
                table: "PersonnelForeignLanguages",
                column: "GidPersonelFK");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelGraduatedSchools_GidPersonelFK",
                table: "PersonnelGraduatedSchools",
                column: "GidPersonelFK");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelPassportInfos_GidPersonelFK",
                table: "PersonnelPassportInfos",
                column: "GidPersonelFK");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelPermitInfos_GidPermitFK",
                table: "PersonnelPermitInfos",
                column: "GidPermitFK");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelPermitInfos_GidPersonelFK",
                table: "PersonnelPermitInfos",
                column: "GidPersonelFK");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelResidenceInfos_GidPersonelFK",
                table: "PersonnelResidenceInfos",
                column: "GidPersonelFK");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelWorkingTables_GidPersonelFK",
                table: "PersonnelWorkingTables",
                column: "GidPersonelFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "DepartmentUsers");

            migrationBuilder.DropTable(
                name: "JobTypes");

            migrationBuilder.DropTable(
                name: "MeasureTypes");

            migrationBuilder.DropTable(
                name: "OtoBrands");

            migrationBuilder.DropTable(
                name: "PersonnelAddresses");

            migrationBuilder.DropTable(
                name: "PersonnelDocuments");

            migrationBuilder.DropTable(
                name: "PersonnelForeignLanguages");

            migrationBuilder.DropTable(
                name: "PersonnelGraduatedSchools");

            migrationBuilder.DropTable(
                name: "PersonnelPassportInfos");

            migrationBuilder.DropTable(
                name: "PersonnelPermitInfos");

            migrationBuilder.DropTable(
                name: "PersonnelResidenceInfos");

            migrationBuilder.DropTable(
                name: "PersonnelWorkingTables");

            migrationBuilder.DropTable(
                name: "RoomTypes");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropTable(
                name: "ForeignLanguages");

            migrationBuilder.DropTable(
                name: "PermitTypes");

            migrationBuilder.DropColumn(
                name: "RowNo",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "TelefonKodu",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "UlkeAdi",
                table: "Countries");

            migrationBuilder.RenameColumn(
                name: "UlkeKodu",
                table: "Countries",
                newName: "PhoneCode");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Countries",
                type: "varchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Countries",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }
    }
}
