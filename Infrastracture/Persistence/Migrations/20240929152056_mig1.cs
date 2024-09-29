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
                name: "Announcements",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    Link = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    Image = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ShowType = table.Column<int>(type: "int", nullable: false),
                    RowNo = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.Gid);
                });

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
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    CountryCode = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false),
                    PhoneCode = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true),
                    RowNo = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Gid);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    Symbol = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Gid);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
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
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    LanguageCode = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true),
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
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTypes", x => x.Gid);
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
                name: "MeasureTypes",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
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
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
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
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermitTypes", x => x.Gid);
                });

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

            migrationBuilder.CreateTable(
                name: "RoomTypes",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypes", x => x.Gid);
                });

            migrationBuilder.CreateTable(
                name: "SCCompanies",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    WebSite = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Password = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    WebLoginStatus = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    SpecialNote = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    TaxOffice = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    TaxNumber = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Keywords = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true),
                    PartnerType = table.Column<int>(type: "int", nullable: false),
                    SupplierRank = table.Column<int>(type: "int", nullable: true),
                    CustomerRank = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SCCompanies", x => x.Gid);
                });

            migrationBuilder.CreateTable(
                name: "StockCards",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StockCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    StockName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    TaxRate = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(1500)", maxLength: 1500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockCards", x => x.Gid);
                });

            migrationBuilder.CreateTable(
                name: "TaskGroups",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskGroups", x => x.Gid);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Gid);
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
                name: "Cities",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidCountryFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    PlateCode = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_GidCountryFK",
                        column: x => x.GidCountryFK,
                        principalTable: "Countries",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidNationalityFK = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Avatar = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Title = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true),
                    Password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    UpdatePasswordToken = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    TokenExpiredDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsLoginStatus = table.Column<bool>(type: "bit", nullable: false),
                    IsSystemAdmin = table.Column<bool>(type: "bit", nullable: false),
                    Gsm = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Birthplace = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IdentityNo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    PassportNo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    SGKNo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    DrivingLicenseNo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Note = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true),
                    MaritalStatus = table.Column<int>(type: "int", nullable: true),
                    BloodGroup = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    EmailActivationStatus = table.Column<int>(type: "int", nullable: false),
                    SmsActivationStatus = table.Column<int>(type: "int", nullable: false),
                    PersonnelSpecialNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_Users_Countries_GidNationalityFK",
                        column: x => x.GidNationalityFK,
                        principalTable: "Countries",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SCBanks",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidSCCompanyFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidCurrencyFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Bank = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    BranchName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    BranchCode = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    AccountNumber = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    IbanNo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    SwiftNo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SCBanks", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_SCBanks_Currencies_GidCurrencyFK",
                        column: x => x.GidCurrencyFK,
                        principalTable: "Currencies",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SCBanks_SCCompanies_GidSCCompanyFK",
                        column: x => x.GidSCCompanyFK,
                        principalTable: "SCCompanies",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SCEmployers",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidSCCompanyFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Duty = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    SpecialNote = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SCEmployers", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_SCEmployers_SCCompanies_GidSCCompanyFK",
                        column: x => x.GidSCCompanyFK,
                        principalTable: "SCCompanies",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SCWorkHistories",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidSCCompanyFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Detail = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    WorkDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    WorkFile = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SCWorkHistories", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_SCWorkHistories_SCCompanies_GidSCCompanyFK",
                        column: x => x.GidSCCompanyFK,
                        principalTable: "SCCompanies",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockCardImages",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidStockCardFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Image = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    RowNo = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockCardImages", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_StockCardImages_StockCards_GidStockCardFK",
                        column: x => x.GidStockCardFK,
                        principalTable: "StockCards",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockMovements",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidStockCardFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidPreviousWarehouseFK = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GidNextWarehouseFK = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OperationType = table.Column<int>(type: "int", nullable: false),
                    MovementType = table.Column<int>(type: "int", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Document = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockMovements", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_StockMovements_StockCards_GidStockCardFK",
                        column: x => x.GidStockCardFK,
                        principalTable: "StockCards",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockMovements_Warehouses_GidNextWarehouseFK",
                        column: x => x.GidNextWarehouseFK,
                        principalTable: "Warehouses",
                        principalColumn: "Gid");
                    table.ForeignKey(
                        name: "FK_StockMovements_Warehouses_GidPreviousWarehouseFK",
                        column: x => x.GidPreviousWarehouseFK,
                        principalTable: "Warehouses",
                        principalColumn: "Gid");
                });

            migrationBuilder.CreateTable(
                name: "SCAddresses",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidSCCompanyFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidCityFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    District = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    PostalCode = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SCAddresses", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_SCAddresses_Cities_GidCityFK",
                        column: x => x.GidCityFK,
                        principalTable: "Cities",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SCAddresses_SCCompanies_GidSCCompanyFK",
                        column: x => x.GidSCCompanyFK,
                        principalTable: "SCCompanies",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnnouncementRecipients",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidAnnouncementFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidRecipientFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReadDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReadIpAddress = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Confirm = table.Column<bool>(type: "bit", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementRecipients", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_AnnouncementRecipients_Announcements_GidAnnouncementFK",
                        column: x => x.GidAnnouncementFK,
                        principalTable: "Announcements",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnouncementRecipients_Users_GidRecipientFK",
                        column: x => x.GidRecipientFK,
                        principalTable: "Users",
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
                name: "Departments",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidMainAdminFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidCoAdminFK = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Details = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_Departments_Users_GidCoAdminFK",
                        column: x => x.GidCoAdminFK,
                        principalTable: "Users",
                        principalColumn: "Gid");
                    table.ForeignKey(
                        name: "FK_Departments_Users_GidMainAdminFK",
                        column: x => x.GidMainAdminFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Cascade);
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
                name: "PersonnelAddresses",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidPersonnelFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidCityFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressTitle = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelAddresses", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_PersonnelAddresses_Cities_GidCityFK",
                        column: x => x.GidCityFK,
                        principalTable: "Cities",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonnelAddresses_Users_GidPersonnelFK",
                        column: x => x.GidPersonnelFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonnelDocuments",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidPersonnelFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidDocumentType = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    ValidityDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Document = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Description = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelDocuments", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_PersonnelDocuments_DocumentTypes_GidDocumentType",
                        column: x => x.GidDocumentType,
                        principalTable: "DocumentTypes",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonnelDocuments_Users_GidPersonnelFK",
                        column: x => x.GidPersonnelFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonnelForeignLanguages",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidPersonnelFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidLanguageFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpeakingLevel = table.Column<int>(type: "int", nullable: false),
                    ReadLevel = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_PersonnelForeignLanguages_Users_GidPersonnelFK",
                        column: x => x.GidPersonnelFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonnelGraduatedSchools",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidPersonnelFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EducationalInstitutionType = table.Column<int>(type: "int", nullable: false),
                    SchoolInfo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    DepartmentInfo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    StartYear = table.Column<int>(type: "int", nullable: false),
                    GraduationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Document = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Description = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelGraduatedSchools", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_PersonnelGraduatedSchools_Users_GidPersonnelFK",
                        column: x => x.GidPersonnelFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonnelPassportInfos",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidPersonnelFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PassportNo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    DateOfIssue = table.Column<DateTime>(type: "datetime", nullable: false),
                    ValidityDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Document = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Description = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelPassportInfos", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_PersonnelPassportInfos_Users_GidPersonnelFK",
                        column: x => x.GidPersonnelFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonnelPermitInfos",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidPersonnelFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidPermitFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermitStartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    PermitEndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Document = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Description = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
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
                        name: "FK_PersonnelPermitInfos_Users_GidPersonnelFK",
                        column: x => x.GidPersonnelFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonnelResidenceInfos",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidPersonnelFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessionSerialNo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    DateOfIssue = table.Column<DateTime>(type: "datetime", nullable: false),
                    ValidityDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Document = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Description = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelResidenceInfos", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_PersonnelResidenceInfos_Users_GidPersonnelFK",
                        column: x => x.GidPersonnelFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonnelWorkingTables",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidPersonnelFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ExitDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelWorkingTables", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_PersonnelWorkingTables_Users_GidPersonnelFK",
                        column: x => x.GidPersonnelFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupportRequests",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUserFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    SupportStatus = table.Column<int>(type: "int", nullable: false),
                    PriorityType = table.Column<int>(type: "int", nullable: false),
                    SupportType = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportRequests", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_SupportRequests_Users_CreatedUserFK",
                        column: x => x.CreatedUserFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskGroupUsers",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidTaskGroupFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidUserFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskGroupUsers", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_TaskGroupUsers_TaskGroups_GidTaskGroupFK",
                        column: x => x.GidTaskGroupFK,
                        principalTable: "TaskGroups",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskGroupUsers_Users_GidUserFK",
                        column: x => x.GidUserFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskManagers",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidUserFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskManagers", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_TaskManagers_Users_GidUserFK",
                        column: x => x.GidUserFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidTaskAssignerUserFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    PriorityType = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_Tasks_Users_GidTaskAssignerUserFK",
                        column: x => x.GidTaskAssignerUserFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserModuleAuths",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidUserFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModuleType = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserModuleAuths", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_UserModuleAuths_Users_GidUserFK",
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

            migrationBuilder.CreateTable(
                name: "UserShortCuts",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidUserFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PageName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    PageUrl = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    RowNo = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserShortCuts", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_UserShortCuts_Users_GidUserFK",
                        column: x => x.GidUserFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentUsers",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidDepartmentFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidPersonnelFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentUsers", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_DepartmentUsers_Departments_GidDepartmentFK",
                        column: x => x.GidDepartmentFK,
                        principalTable: "Departments",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepartmentUsers_Users_GidPersonnelFK",
                        column: x => x.GidPersonnelFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupportMessages",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidSupportFK = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GidSenderUserFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Message = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    MessageType = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportMessages", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_SupportMessages_SupportRequests_GidSupportFK",
                        column: x => x.GidSupportFK,
                        principalTable: "SupportRequests",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupportMessages_Users_GidSenderUserFK",
                        column: x => x.GidSenderUserFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskComments",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidUserFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidTaskFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comment = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskComments", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_TaskComments_Tasks_GidTaskFK",
                        column: x => x.GidTaskFK,
                        principalTable: "Tasks",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskComments_Users_GidUserFK",
                        column: x => x.GidUserFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskFiles",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidTaskFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidFileUploadUserFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileTitle = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    FileDescription = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    UploadedFile = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskFiles", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_TaskFiles_Tasks_GidTaskFK",
                        column: x => x.GidTaskFK,
                        principalTable: "Tasks",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskFiles_Users_GidFileUploadUserFK",
                        column: x => x.GidFileUploadUserFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskUsers",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidUserFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidTaskFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskState = table.Column<int>(type: "int", nullable: false),
                    StatusNote = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskUsers", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_TaskUsers_Tasks_GidTaskFK",
                        column: x => x.GidTaskFK,
                        principalTable: "Tasks",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskUsers_Users_GidUserFK",
                        column: x => x.GidUserFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupportMessageDetails",
                columns: table => new
                {
                    Gid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidMessageFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GidReadUserFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReadDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ReadIp = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportMessageDetails", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_SupportMessageDetails_SupportMessages_GidMessageFK",
                        column: x => x.GidMessageFK,
                        principalTable: "SupportMessages",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupportMessageDetails_Users_GidReadUserFK",
                        column: x => x.GidReadUserFK,
                        principalTable: "Users",
                        principalColumn: "Gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementRecipients_GidAnnouncementFK",
                table: "AnnouncementRecipients",
                column: "GidAnnouncementFK");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementRecipients_GidRecipientFK",
                table: "AnnouncementRecipients",
                column: "GidRecipientFK");

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
                name: "IX_Cities_GidCountryFK",
                table: "Cities",
                column: "GidCountryFK");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_GidCoAdminFK",
                table: "Departments",
                column: "GidCoAdminFK");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_GidMainAdminFK",
                table: "Departments",
                column: "GidMainAdminFK");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentUsers_GidDepartmentFK",
                table: "DepartmentUsers",
                column: "GidDepartmentFK");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentUsers_GidPersonnelFK",
                table: "DepartmentUsers",
                column: "GidPersonnelFK");

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
                name: "IX_PersonnelAddresses_GidCityFK",
                table: "PersonnelAddresses",
                column: "GidCityFK");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelAddresses_GidPersonnelFK",
                table: "PersonnelAddresses",
                column: "GidPersonnelFK");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelDocuments_GidDocumentType",
                table: "PersonnelDocuments",
                column: "GidDocumentType");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelDocuments_GidPersonnelFK",
                table: "PersonnelDocuments",
                column: "GidPersonnelFK");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelForeignLanguages_GidLanguageFK",
                table: "PersonnelForeignLanguages",
                column: "GidLanguageFK");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelForeignLanguages_GidPersonnelFK",
                table: "PersonnelForeignLanguages",
                column: "GidPersonnelFK");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelGraduatedSchools_GidPersonnelFK",
                table: "PersonnelGraduatedSchools",
                column: "GidPersonnelFK");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelPassportInfos_GidPersonnelFK",
                table: "PersonnelPassportInfos",
                column: "GidPersonnelFK");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelPermitInfos_GidPermitFK",
                table: "PersonnelPermitInfos",
                column: "GidPermitFK");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelPermitInfos_GidPersonnelFK",
                table: "PersonnelPermitInfos",
                column: "GidPersonnelFK");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelResidenceInfos_GidPersonnelFK",
                table: "PersonnelResidenceInfos",
                column: "GidPersonnelFK");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelWorkingTables_GidPersonnelFK",
                table: "PersonnelWorkingTables",
                column: "GidPersonnelFK");

            migrationBuilder.CreateIndex(
                name: "IX_SCAddresses_GidCityFK",
                table: "SCAddresses",
                column: "GidCityFK");

            migrationBuilder.CreateIndex(
                name: "IX_SCAddresses_GidSCCompanyFK",
                table: "SCAddresses",
                column: "GidSCCompanyFK");

            migrationBuilder.CreateIndex(
                name: "IX_SCBanks_GidCurrencyFK",
                table: "SCBanks",
                column: "GidCurrencyFK");

            migrationBuilder.CreateIndex(
                name: "IX_SCBanks_GidSCCompanyFK",
                table: "SCBanks",
                column: "GidSCCompanyFK");

            migrationBuilder.CreateIndex(
                name: "IX_SCEmployers_GidSCCompanyFK",
                table: "SCEmployers",
                column: "GidSCCompanyFK");

            migrationBuilder.CreateIndex(
                name: "IX_SCWorkHistories_GidSCCompanyFK",
                table: "SCWorkHistories",
                column: "GidSCCompanyFK");

            migrationBuilder.CreateIndex(
                name: "IX_StockCardImages_GidStockCardFK",
                table: "StockCardImages",
                column: "GidStockCardFK");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_GidNextWarehouseFK",
                table: "StockMovements",
                column: "GidNextWarehouseFK");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_GidPreviousWarehouseFK",
                table: "StockMovements",
                column: "GidPreviousWarehouseFK");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_GidStockCardFK",
                table: "StockMovements",
                column: "GidStockCardFK");

            migrationBuilder.CreateIndex(
                name: "IX_SupportMessageDetails_GidMessageFK",
                table: "SupportMessageDetails",
                column: "GidMessageFK");

            migrationBuilder.CreateIndex(
                name: "IX_SupportMessageDetails_GidReadUserFK",
                table: "SupportMessageDetails",
                column: "GidReadUserFK");

            migrationBuilder.CreateIndex(
                name: "IX_SupportMessages_GidSenderUserFK",
                table: "SupportMessages",
                column: "GidSenderUserFK");

            migrationBuilder.CreateIndex(
                name: "IX_SupportMessages_GidSupportFK",
                table: "SupportMessages",
                column: "GidSupportFK");

            migrationBuilder.CreateIndex(
                name: "IX_SupportRequests_CreatedUserFK",
                table: "SupportRequests",
                column: "CreatedUserFK");

            migrationBuilder.CreateIndex(
                name: "IX_TaskComments_GidTaskFK",
                table: "TaskComments",
                column: "GidTaskFK");

            migrationBuilder.CreateIndex(
                name: "IX_TaskComments_GidUserFK",
                table: "TaskComments",
                column: "GidUserFK");

            migrationBuilder.CreateIndex(
                name: "IX_TaskFiles_GidFileUploadUserFK",
                table: "TaskFiles",
                column: "GidFileUploadUserFK");

            migrationBuilder.CreateIndex(
                name: "IX_TaskFiles_GidTaskFK",
                table: "TaskFiles",
                column: "GidTaskFK");

            migrationBuilder.CreateIndex(
                name: "IX_TaskGroupUsers_GidTaskGroupFK",
                table: "TaskGroupUsers",
                column: "GidTaskGroupFK");

            migrationBuilder.CreateIndex(
                name: "IX_TaskGroupUsers_GidUserFK",
                table: "TaskGroupUsers",
                column: "GidUserFK");

            migrationBuilder.CreateIndex(
                name: "IX_TaskManagers_GidUserFK",
                table: "TaskManagers",
                column: "GidUserFK");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_GidTaskAssignerUserFK",
                table: "Tasks",
                column: "GidTaskAssignerUserFK");

            migrationBuilder.CreateIndex(
                name: "IX_TaskUsers_GidTaskFK",
                table: "TaskUsers",
                column: "GidTaskFK");

            migrationBuilder.CreateIndex(
                name: "IX_TaskUsers_GidUserFK",
                table: "TaskUsers",
                column: "GidUserFK");

            migrationBuilder.CreateIndex(
                name: "IX_UserModuleAuths_GidUserFK",
                table: "UserModuleAuths",
                column: "GidUserFK");

            migrationBuilder.CreateIndex(
                name: "IX_UserRefreshTokens_GidUserFK",
                table: "UserRefreshTokens",
                column: "GidUserFK");

            migrationBuilder.CreateIndex(
                name: "IX_Users_GidNationalityFK",
                table: "Users",
                column: "GidNationalityFK");

            migrationBuilder.CreateIndex(
                name: "IX_UserShortCuts_GidUserFK",
                table: "UserShortCuts",
                column: "GidUserFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnouncementRecipients");

            migrationBuilder.DropTable(
                name: "AuthRolePages");

            migrationBuilder.DropTable(
                name: "AuthUserRoles");

            migrationBuilder.DropTable(
                name: "DepartmentUsers");

            migrationBuilder.DropTable(
                name: "JobTypes");

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
                name: "PortalParameters");

            migrationBuilder.DropTable(
                name: "PortalTexts");

            migrationBuilder.DropTable(
                name: "RoomTypes");

            migrationBuilder.DropTable(
                name: "SCAddresses");

            migrationBuilder.DropTable(
                name: "SCBanks");

            migrationBuilder.DropTable(
                name: "SCEmployers");

            migrationBuilder.DropTable(
                name: "SCWorkHistories");

            migrationBuilder.DropTable(
                name: "StockCardImages");

            migrationBuilder.DropTable(
                name: "StockMovements");

            migrationBuilder.DropTable(
                name: "SupportMessageDetails");

            migrationBuilder.DropTable(
                name: "TaskComments");

            migrationBuilder.DropTable(
                name: "TaskFiles");

            migrationBuilder.DropTable(
                name: "TaskGroupUsers");

            migrationBuilder.DropTable(
                name: "TaskManagers");

            migrationBuilder.DropTable(
                name: "TaskUsers");

            migrationBuilder.DropTable(
                name: "UserModuleAuths");

            migrationBuilder.DropTable(
                name: "UserRefreshTokens");

            migrationBuilder.DropTable(
                name: "UserShortCuts");

            migrationBuilder.DropTable(
                name: "Announcements");

            migrationBuilder.DropTable(
                name: "AuthPages");

            migrationBuilder.DropTable(
                name: "AuthRoles");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropTable(
                name: "ForeignLanguages");

            migrationBuilder.DropTable(
                name: "PermitTypes");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "SCCompanies");

            migrationBuilder.DropTable(
                name: "StockCards");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "SupportMessages");

            migrationBuilder.DropTable(
                name: "TaskGroups");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "SupportRequests");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
