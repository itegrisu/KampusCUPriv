using Core.Entities;
using Domain.Entities.AnnouncementManagements;
using Domain.Entities.AuthManagements;
using Domain.Entities.DefinitionManagements;
using Domain.Entities.FinanceManagements;
using Domain.Entities.GeneralManagements;
using Domain.Entities.LogManagements;
using Domain.Entities.MarketingManagements;
using Domain.Entities.OfferManagements;
using Domain.Entities.OrganizationManagements;
using Domain.Entities.PersonnelManagements;
using Domain.Entities.PortalManagements;
using Domain.Entities.SupplierCustomerManagements;
using Domain.Entities.SupportManagements;
using Domain.Entities.TaskManagements;
using Domain.Entities.VehicleManagements;
using Domain.Entities.WarehouseManagements;
using Microsoft.EntityFrameworkCore;
using OrganizationManagement.Persistence.EntityConfiguration;
using Persistence.EntityConfiguration.AnnouncementManagements;
using Persistence.EntityConfiguration.AuthManagements;
using Persistence.EntityConfiguration.DefinitionManagements;
using Persistence.EntityConfiguration.FinanceManagements;
using Persistence.EntityConfiguration.GeneralManagements;
using Persistence.EntityConfiguration.LogManagements;
using Persistence.EntityConfiguration.MarketingManagements;
using Persistence.EntityConfiguration.OfferManagements;
using Persistence.EntityConfiguration.OrganisationManagements;
using Persistence.EntityConfiguration.PersonnelManagements;
using Persistence.EntityConfiguration.PortalManagements;
using Persistence.EntityConfiguration.SupplierManagements;
using Persistence.EntityConfiguration.SupportManagements;
using Persistence.EntityConfiguration.TaskManagements;
using Persistence.EntityConfiguration.VehicleManagements;
using Persistence.EntityConfiguration.WareHouseManagements;
using C = Core.Context;
using T = Domain.Entities.TaskManagements;

namespace Persistence.Context
{
    public class Emasist2024Context : C.Context
    {


        public Emasist2024Context(DbContextOptions<Emasist2024Context> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;  //introducing hatasý için yazýldý
            }


            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRefreshTokenConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentUserConfiguration());
            modelBuilder.ApplyConfiguration(new UserShortCutConfiguration());
            modelBuilder.ApplyConfiguration(new UserModuleAuthConfiguration());

            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new CityConfiguration());
            modelBuilder.ApplyConfiguration(new PermitTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ForeignLanguageConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CurrencyConfiguration());
            modelBuilder.ApplyConfiguration(new JobTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OtoBrandConfiguration());
            modelBuilder.ApplyConfiguration(new RoomTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MeasureTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StockCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new OrganizationTypeConfiguration());


            modelBuilder.ApplyConfiguration(new AuthUserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new AuthRoleConfiguration());
            modelBuilder.ApplyConfiguration(new AuthPageConfiguration());
            modelBuilder.ApplyConfiguration(new AuthRolePageConfiguration());

            modelBuilder.ApplyConfiguration(new LogFailedLoginConfiguration());
            modelBuilder.ApplyConfiguration(new LogSuccessedLoginConfiguration());
            modelBuilder.ApplyConfiguration(new LogAuthorizationErrorConfiguration());
            modelBuilder.ApplyConfiguration(new LogUserPageVisitConfiguration());
            modelBuilder.ApplyConfiguration(new LogUserPageVisitActionConfiguration());
            modelBuilder.ApplyConfiguration(new LogEmailSendConfiguration());

            modelBuilder.ApplyConfiguration(new PortalParameterConfiguration());
            modelBuilder.ApplyConfiguration(new PortalTextConfiguration());

            modelBuilder.ApplyConfiguration(new MarketingCustomerConfiguration());
            modelBuilder.ApplyConfiguration(new MarketingVisitPlanConfiguration());

            modelBuilder.ApplyConfiguration(new AnnouncementConfiguration());
            modelBuilder.ApplyConfiguration(new AnnouncementRecipientConfiguration());

            modelBuilder.ApplyConfiguration(new TaskConfiguration());
            modelBuilder.ApplyConfiguration(new TaskUserConfiguration());
            modelBuilder.ApplyConfiguration(new TaskCommentConfiguration());
            modelBuilder.ApplyConfiguration(new TaskFileConfiguration());


            modelBuilder.ApplyConfiguration(new SupportRequestConfiguration());
            modelBuilder.ApplyConfiguration(new SupportMessageConfiguration());
            modelBuilder.ApplyConfiguration(new SupportMessageDetailConfiguration());

            modelBuilder.ApplyConfiguration(new StockCardConfiguration());
            modelBuilder.ApplyConfiguration(new StockCardImageConfiguration());
            modelBuilder.ApplyConfiguration(new StockMovementConfiguration());
            modelBuilder.ApplyConfiguration(new WarehouseConfiguration());

            modelBuilder.ApplyConfiguration(new PersonnelAddressConfiguration());
            modelBuilder.ApplyConfiguration(new PersonnelWorkingTableConfiguration());
            modelBuilder.ApplyConfiguration(new PersonnelPermitInfoConfiguration());
            modelBuilder.ApplyConfiguration(new PersonnelForeignLanguageConfiguration());
            modelBuilder.ApplyConfiguration(new PersonnelGraduatedSchoolConfiguration());
            modelBuilder.ApplyConfiguration(new PersonnelPassportInfoConfiguration());
            modelBuilder.ApplyConfiguration(new PersonnelResidenceInfoConfiguration());
            modelBuilder.ApplyConfiguration(new PersonnelDocumentConfiguration());

            modelBuilder.ApplyConfiguration(new SCCompanyConfiguration());
            modelBuilder.ApplyConfiguration(new SCAddressConfiguration());
            modelBuilder.ApplyConfiguration(new SCBankConfiguration());
            modelBuilder.ApplyConfiguration(new SCEmployerConfiguration());
            modelBuilder.ApplyConfiguration(new SCWorkHistoryConfiguration());

            modelBuilder.ApplyConfiguration(new OfferConfiguration());
            modelBuilder.ApplyConfiguration(new OfferFileConfiguration());
            modelBuilder.ApplyConfiguration(new OfferTransactionConfiguration());


            modelBuilder.ApplyConfiguration(new OrganizationItemConfiguration());
            modelBuilder.ApplyConfiguration(new OrganizationConfiguration());
            modelBuilder.ApplyConfiguration(new OrganizationGroupConfiguration());
            modelBuilder.ApplyConfiguration(new OrganizationItemMessageConfiguration());
            modelBuilder.ApplyConfiguration(new OrganizationItemFileConfiguration());

            modelBuilder.ApplyConfiguration(new FinanceIncomeGroupConfiguration());
            modelBuilder.ApplyConfiguration(new FinanceIncomeConfiguration());
            modelBuilder.ApplyConfiguration(new FinanceExpenseGroupConfiguration());
            modelBuilder.ApplyConfiguration(new FinanceExpenseConfiguration());
            modelBuilder.ApplyConfiguration(new FinanceExpenseDetailConfiguration());

            modelBuilder.ApplyConfiguration(new VehicleAllConfiguration());
            modelBuilder.ApplyConfiguration(new VehicleTransactionConfiguration());
            modelBuilder.ApplyConfiguration(new VehicleDocumentConfiguration());
            modelBuilder.ApplyConfiguration(new VehicleInsuranceConfiguration());
            modelBuilder.ApplyConfiguration(new VehicleMaintenanceConfiguration());
            modelBuilder.ApplyConfiguration(new VehicleInspectionConfiguration());
            modelBuilder.ApplyConfiguration(new VehicleEquipmentConfiguration());
            modelBuilder.ApplyConfiguration(new VehicleFuelConfiguration());
            modelBuilder.ApplyConfiguration(new TyreConfiguration());
            modelBuilder.ApplyConfiguration(new TyreTypeConfiguration());
            modelBuilder.ApplyConfiguration(new VehicleTyreUseConfiguration());
            modelBuilder.ApplyConfiguration(new VehicleRequestConfiguration());



            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //ChangeTracker : Entityler üzerinden yapýlan deðiþiklerin ya da yeni eklenen verinin yakalanmasýný saðlayan propertydir. Update operasyonlarýnda Track edilen verileri yakalayýp elde etmemizi saðlar.

            var datas = ChangeTracker
                  .Entries<BaseEntity>();

            foreach (var data in datas)
            {

                switch (data.State)
                {
                    case EntityState.Modified:
                        //data.Entity.DataState = Domain.Enums.DataState.Active;
                        if (data.Entity.DataState != Core.Enum.DataState.Deleted)
                        {
                            data.Entity.DataState = data.Entity.DataState;
                        }

                        break;
                    case EntityState.Added:
                        if (data.Entity.DataState != Core.Enum.DataState.None)
                        {
                            data.Entity.DataState = data.Entity.DataState;
                            data.Entity.CreatedDate = DateTime.Now;
                            break;
                        }
                        data.Entity.CreatedDate = DateTime.Now;
                        data.Entity.DataState = Core.Enum.DataState.Active;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);

        }


        // DbSet, veritabaný tablosu üzerinde CRUD iþlemlerini gerçekleþtirmeyi saðlar
        public DbSet<User> Users { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<LogAuthorizationError> LogAuthorizationErrors { get; set; }
        public DbSet<LogFailedLogin> LogFailedLogins { get; set; }
        public DbSet<LogSuccessedLogin> LogSuccessedLogins { get; set; }
        public DbSet<LogUserPageVisit> LogUserPageVisits { get; set; }
        public DbSet<LogUserPageVisitAction> LogUserPageVisitActions { get; set; }
        public DbSet<LogEmailSend> LogEmailSends { get; set; }
        public DbSet<AuthRole> AuthRoles { get; set; }
        public DbSet<AuthUserRole> AuthUserRoles { get; set; }
        public DbSet<AuthRolePage> AuthRolePages { get; set; }
        public DbSet<AuthPage> AuthPages { get; set; }
        public DbSet<PortalParameter> PortalParameters { get; set; }
        public DbSet<MarketingCustomer> MarketingCustomers { get; set; }
        public DbSet<MarketingVisitPlan> MarketingVisitPlans { get; set; }
        public DbSet<PortalText> PortalTexts { get; set; }
        public DbSet<SupportRequest> SupportRequests { get; set; }
        public DbSet<SupportMessage> SupportMessages { get; set; }
        public DbSet<SupportMessageDetail> SupportMessageDetails { get; set; }
        public DbSet<StockCard> StockCards { get; set; }
        public DbSet<StockCardImage> StockCardImages { get; set; }
        public DbSet<TaskComment> TaskComments { get; set; }
        public DbSet<TaskFile> TaskFiles { get; set; }
        public DbSet<StockMovement> StockMovements { get; set; }
        public DbSet<T.Task> Tasks { get; set; }
        public DbSet<TaskUser> TaskUsers { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<AnnouncementRecipient> AnnouncementRecipients { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentUser> DepartmentUsers { get; set; }
        public DbSet<PersonnelAddress> PersonnelAddresses { get; set; }
        public DbSet<PersonnelWorkingTable> PersonnelWorkingTables { get; set; }
        public DbSet<PersonnelPermitInfo> PersonnelPermitInfos { get; set; }
        public DbSet<PersonnelForeignLanguage> PersonnelForeignLanguages { get; set; }
        public DbSet<PersonnelGraduatedSchool> PersonnelGraduatedSchools { get; set; }
        public DbSet<PersonnelPassportInfo> PersonnelPassportInfos { get; set; }
        public DbSet<PersonnelResidenceInfo> PersonnelResidenceInfos { get; set; }
        public DbSet<PersonnelDocument> PersonnelDocuments { get; set; }
        public DbSet<UserShortCut> UserShortCuts { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<PermitType> PermitTypes { get; set; }
        public DbSet<ForeignLanguage> ForeignLanguages { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<JobType> JobTypes { get; set; }
        public DbSet<OtoBrand> OtoBrands { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<MeasureType> MeasureTypes { get; set; }
        public DbSet<SCCompany> SCCompanies { get; set; }
        public DbSet<SCAddress> SCAddresses { get; set; }
        public DbSet<SCBank> SCBanks { get; set; }
        public DbSet<SCEmployer> SCEmployers { get; set; }
        public DbSet<SCWorkHistory> SCWorkHistories { get; set; }
        public DbSet<UserModuleAuth> UserModuleAuths { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<OfferFile> OfferFiles { get; set; }
        public DbSet<OfferTransaction> OfferTransactions { get; set; }
        public DbSet<OrganizationType> OrganizationTypes { get; set; }
        public DbSet<OrganizationItem> OrganizationItems { get; set; }
        public DbSet<OrganizationItemMessage> OrganizationItemMessages { get; set; }
        public DbSet<OrganizationItemFile> OrganizationItemFiles { get; set; }
        public DbSet<FinanceIncomeGroup> FinanceIncomeGroups { get; set; }
        public DbSet<FinanceIncome> FinanceIncomes { get; set; }
        public DbSet<FinanceExpenseGroup> FinanceExpenseGroups { get; set; }
        public DbSet<FinanceExpense> FinanceExpenses { get; set; }
        public DbSet<FinanceExpenseDetail> FinanceExpenseDetails { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OrganizationGroup> OrganizationGroups { get; set; }
        public DbSet<StockCategory> StockCategories { get; set; }
        public DbSet<VehicleAll> VehicleAlls { get; set; }
        public DbSet<VehicleTransaction> VehicleTransactions { get; set; }
        public DbSet<VehicleDocument> VehicleDocuments { get; set; }
        public DbSet<VehicleInsurance> VehicleInsurances { get; set; }
        public DbSet<VehicleMaintenance> VehicleMaintenances { get; set; }
        public DbSet<VehicleInspection> VehicleInspections { get; set; }
        public DbSet<VehicleEquipment> VehicleEquipments { get; set; }
        public DbSet<VehicleFuel> VehicleFuels { get; set; }
        public DbSet<Tyre> Tyres { get; set; }
        public DbSet<TyreType> TyreTypes { get; set; }
        public DbSet<VehicleTyreUse> VehicleTyreUses { get; set; }
        public DbSet<VehicleRequest> VehicleRequests { get; set; }
    }
}
