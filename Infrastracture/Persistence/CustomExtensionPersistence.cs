using Application.Abstractions.EntityServices;
using Application.Abstractions.UnitOfWork;
using Application.Repositories.AnnouncementManagementRepos.AnnouncementRecipientRepo;
using Application.Repositories.AnnouncementManagementRepos.AnnouncementRepo;
using Application.Repositories.AuthManagementRepos.AuthPageRepo;
using Application.Repositories.AuthManagementRepos.AuthRolePageRepo;
using Application.Repositories.AuthManagementRepos.AuthRoleRepo;
using Application.Repositories.AuthManagementRepos.AuthUserRoleRepo;
using Application.Repositories.DefinitionManagementRepos.CityRepo;
using Application.Repositories.DefinitionManagementRepos.CountryRepo;
using Application.Repositories.DefinitionManagementRepos.CurrencyRepo;
using Application.Repositories.DefinitionManagementRepos.DocumentTypeRepo;
using Application.Repositories.DefinitionManagementRepos.ForeignLanguageRepo;
using Application.Repositories.DefinitionManagementRepos.JopTypeRepo;
using Application.Repositories.DefinitionManagementRepos.MeasureTypeRepo;
using Application.Repositories.DefinitionManagementRepos.OrganizationTypeRepo;
using Application.Repositories.DefinitionManagementRepos.OtoBrandRepo;
using Application.Repositories.DefinitionManagementRepos.PermitTypeRepo;
using Application.Repositories.DefinitionManagementRepos.RoomTypeRepo;
using Application.Repositories.DefinitionManagementRepos.StockCategoryRepo;
using Application.Repositories.FinanceManagementRepos.FinanceExpenseDetailRepo;
using Application.Repositories.FinanceManagementRepos.FinanceExpenseGroupRepo;
using Application.Repositories.FinanceManagementRepos.FinanceExpenseRepo;
using Application.Repositories.FinanceManagementRepos.FinanceIncomeGroupRepo;
using Application.Repositories.FinanceManagementRepos.FinanceIncomeRepo;
using Application.Repositories.GeneralManagementRepos.DepartmentRepo;
using Application.Repositories.GeneralManagementRepos.DepartmentUserRepo;
using Application.Repositories.GeneralManagementRepos.UserModuleAuthRepo;
using Application.Repositories.GeneralManagementRepos.UserRefreshTokenRepo;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Application.Repositories.GeneralManagementRepos.UserShortCutRepo;
using Application.Repositories.LogManagementRepos.LogAuthorizationErrorRepo;
using Application.Repositories.LogManagementRepos.LogEmailSendRepo;
using Application.Repositories.LogManagementRepos.LogFailedLoginRepo;
using Application.Repositories.LogManagementRepos.LogSuccessedLoginRepo;
using Application.Repositories.LogManagementRepos.LogUserPageVisitActionRepo;
using Application.Repositories.LogManagementRepos.LogUserPageVisitRepo;
using Application.Repositories.MarketingManagementsRepos.MarketingCustomerRepo;
using Application.Repositories.MarketingManagementsRepos.MerketingVisitPlanRepo;
using Application.Repositories.OfferManagementRepos.OfferFileRepo;
using Application.Repositories.OfferManagementRepos.OfferRepo;
using Application.Repositories.OfferManagementRepos.OfferTransactionRepo;
using Application.Repositories.OrganizationManagementRepos.OrganizationFileRepo;
using Application.Repositories.OrganizationManagementRepos.OrganizationGroupRepo;
using Application.Repositories.OrganizationManagementRepos.OrganizationItemRepo;
using Application.Repositories.OrganizationManagementRepos.OrganizationRepo;
using Application.Repositories.PersonnelManagementRepos.PersonnelAddressRepo;
using Application.Repositories.PersonnelManagementRepos.PersonnelDocumentRepo;
using Application.Repositories.PersonnelManagementRepos.PersonnelForeignLanguageRepo;
using Application.Repositories.PersonnelManagementRepos.PersonnelGraduatedSchoolRepo;
using Application.Repositories.PersonnelManagementRepos.PersonnelPassportInfoRepo;
using Application.Repositories.PersonnelManagementRepos.PersonnelPermitInfoRepo;
using Application.Repositories.PersonnelManagementRepos.PersonnelResidenceInfoRepo;
using Application.Repositories.PersonnelManagementRepos.PersonnelWorkingTableRepo;
using Application.Repositories.PortalManagementRepos.PortalParameterRepo;
using Application.Repositories.PortalManagementRepos.PortalTextRepo;
using Application.Repositories.SupplierManagementRepos.SCAddressRepo;
using Application.Repositories.SupplierManagementRepos.SCBankRepo;
using Application.Repositories.SupplierManagementRepos.SCCompanyRepo;
using Application.Repositories.SupplierManagementRepos.SCEmployerRepo;
using Application.Repositories.SupplierManagementRepos.SCWorkHistoryRepo;
using Application.Repositories.SupportManagementRepos.SupportMessageDetailRepo;
using Application.Repositories.SupportManagementRepos.SupportMessageRepo;
using Application.Repositories.SupportManagementRepos.SupportRequestRepo;
using Application.Repositories.TaskManagementRepos.TaskCommentRepo;
using Application.Repositories.TaskManagementRepos.TaskFileRepo;
using Application.Repositories.TaskManagementRepos.TaskRepo;
using Application.Repositories.TaskManagementRepos.TaskUserRepo;
using Application.Repositories.WarehouseManagementRepos.StockCardImageRepo;
using Application.Repositories.WarehouseManagementRepos.StockCardRepo;
using Application.Repositories.WarehouseManagementRepos.StockMovementRepo;
using Application.Repositories.WarehouseManagementRepos.WarehouseRepo;
using Core.Repositories.Abstracts;
using Core.Repositories.Concretes;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Repositories.AnnouncementManagementRepos.AnnouncementRecipientRepo;
using Persistence.Repositories.AnnouncementManagementRepos.AnnouncementRepo;
using Persistence.Repositories.AuthManagementRepos.AuthPageRepo;
using Persistence.Repositories.AuthManagementRepos.AuthRolePageRepo;
using Persistence.Repositories.AuthManagementRepos.AuthRoleRepo;
using Persistence.Repositories.AuthManagementRepos.AuthUserRoleRepo;
using Persistence.Repositories.DefinitionManagementRepos.CityRepo;
using Persistence.Repositories.DefinitionManagementRepos.CountryRepo;
using Persistence.Repositories.DefinitionManagementRepos.CurrencyRepo;
using Persistence.Repositories.DefinitionManagementRepos.DocumentTypeRepo;
using Persistence.Repositories.DefinitionManagementRepos.ForeignLanguageRepo;
using Persistence.Repositories.DefinitionManagementRepos.JopTypeRepo;
using Persistence.Repositories.DefinitionManagementRepos.MeasureTypeRepo;
using Persistence.Repositories.DefinitionManagementRepos.OrganizationTypeRepo;
using Persistence.Repositories.DefinitionManagementRepos.OtoBrandRepo;
using Persistence.Repositories.DefinitionManagementRepos.PermitTypeRepo;
using Persistence.Repositories.DefinitionManagementRepos.RoomTypeRepo;
using Persistence.Repositories.DefinitionManagementRepos.StockCategoryRepo;
using Persistence.Repositories.FinanceManagementRepos.FinanceExpenceDetailRepo;
using Persistence.Repositories.FinanceManagementRepos.FinanceExpenseGroupRepo;
using Persistence.Repositories.FinanceManagementRepos.FinanceExpenseRepo;
using Persistence.Repositories.FinanceManagementRepos.FinanceIncomeGroupRepo;
using Persistence.Repositories.FinanceManagementRepos.FinanceIncomeRepo;
using Persistence.Repositories.GeneralManagementRepos.DepartmentRepo;
using Persistence.Repositories.GeneralManagementRepos.DepartmentUserRepo;
using Persistence.Repositories.GeneralManagementRepos.UserModuleAuthRepo;
using Persistence.Repositories.GeneralManagementRepos.UserRefreshTokenRepo;
using Persistence.Repositories.GeneralManagementRepos.UserRepo;
using Persistence.Repositories.GeneralManagementRepos.UserShortCutRepo;
using Persistence.Repositories.LogManagementRepos.LogAuthorizationErrorRepo;
using Persistence.Repositories.LogManagementRepos.LogEmailSendRepo;
using Persistence.Repositories.LogManagementRepos.LogFailedLoginRepo;
using Persistence.Repositories.LogManagementRepos.LogSuccessedLoginRepo;
using Persistence.Repositories.LogManagementRepos.LogUserPageVisitActionRepo;
using Persistence.Repositories.LogManagementRepos.LogUserPageVisitRepo;
using Persistence.Repositories.MarketingManagementRepos.MarketingCustomerRepo;
using Persistence.Repositories.MarketingManagementRepos.MarketingVisitPlanRepo;
using Persistence.Repositories.OfferManagementRepos.OfferFileRepo;
using Persistence.Repositories.OfferManagementRepos.OfferRepo;
using Persistence.Repositories.OfferManagementRepos.OfferTransactionRepo;
using Persistence.Repositories.OrganizationManagementRepos.OrganizationFileRepo;
using Persistence.Repositories.OrganizationManagementRepos.OrganizationGroupRepo;
using Persistence.Repositories.OrganizationManagementRepos.OrganizationItemRepo;
using Persistence.Repositories.OrganizationManagementRepos.OrganizationRepo;
using Persistence.Repositories.PersonnelManagementRepos.PersonelGraduatedSchoolRepo;
using Persistence.Repositories.PersonnelManagementRepos.PersonnelAddressRepo;
using Persistence.Repositories.PersonnelManagementRepos.PersonnelDocumentRepo;
using Persistence.Repositories.PersonnelManagementRepos.PersonnelForeignLanguageRepo;
using Persistence.Repositories.PersonnelManagementRepos.PersonnelPassportInfoRepo;
using Persistence.Repositories.PersonnelManagementRepos.PersonnelPermitInfoRepo;
using Persistence.Repositories.PersonnelManagementRepos.PersonnelResidenceInfoRepo;
using Persistence.Repositories.PersonnelManagementRepos.PersonnelWorkingTableRepo;
using Persistence.Repositories.PortalManagementRepos.PortalParameterRepo;
using Persistence.Repositories.PortalManagementRepos.PortalTextRepo;
using Persistence.Repositories.SupplierManagementRepos.SCAddressRepo;
using Persistence.Repositories.SupplierManagementRepos.SCBankRepo;
using Persistence.Repositories.SupplierManagementRepos.SCCompanyRepo;
using Persistence.Repositories.SupplierManagementRepos.SCEmployerRepo;
using Persistence.Repositories.SupplierManagementRepos.SCWorkHistoryRepo;
using Persistence.Repositories.SupportManagementRepos.SupportMessageDetailRepo;
using Persistence.Repositories.SupportManagementRepos.SupportMessageRepo;
using Persistence.Repositories.SupportManagementRepos.SupportRequestRepo;
using Persistence.Repositories.TaskManagementRepos.TaskCommentRepo;
using Persistence.Repositories.TaskManagementRepos.TaskFileRepo;
using Persistence.Repositories.TaskManagementRepos.TaskRepo;
using Persistence.Repositories.TaskManagementRepos.TaskUserRepo;
using Persistence.Repositories.WarehouseManagementRepos.StockCardImageRepo;
using Persistence.Repositories.WarehouseManagementRepos.StockCardRepo;
using Persistence.Repositories.WarehouseManagementRepos.StockMovementRepo;
using Persistence.Repositories.WarehouseManagementRepos.WarehouseRepo;
using Persistence.Services.EntityServices;
using Persistence.Services.UnitOfWork;
using C = Core.Context;

namespace Persistence
{
    public static class CustomExtensionPersistence
    {
        public static void AddContainerWithDependenciesPersistence(this IServiceCollection services)
        {

            #region User
            services.AddScoped<IUserReadRepository, UserReadRepository>();
            services.AddScoped<IUserWriteRepository, UserWriteRepository>();
            #endregion

            #region UserRefreshToken 
            services.AddScoped<IUserRefreshTokenReadRepository, UserRefreshTokenReadRepository>();
            services.AddScoped<IUserRefreshTokenWriteRepository, UserRefreshTokenWriteRepository>();
            #endregion


            //#region Country
            //services.AddScoped<ICountryReadRepository, CountryReadRepository>();
            //services.AddScoped<ICountryWriteRepository, CountryWriteRepository>();
            //#endregion


            #region logRepo
            services.AddScoped<ILogUserPageVisitActionReadRepository, LogUserPageVisitActionReadRepository>();
            services.AddScoped<ILogUserPageVisitActionWriteRepository, LogUserPageVisitActionWriteRepository>();

            services.AddScoped<ILogUserPageVisitWriteRepository, LogUserPageVisitWriteRepository>();
            services.AddScoped<ILogUserPageVisitReadRepository, LogUserPageVisitReadRepository>();

            services.AddScoped<ILogFailedLoginReadRepository, LogFailedLoginReadRepository>();
            services.AddScoped<ILogFailedLoginWriteRepository, LogFailedLoginWriteRepository>();

            services.AddScoped<ILogAuthorizationErrorReadRepository, LogAuthorizationErrorReadRepository>();
            services.AddScoped<ILogAuthorizationErrorWriteRepository, LogAuthorizationErrorWriteRepository>();

            services.AddScoped<ILogSuccessedLoginReadRepository, LogSuccessedLoginReadRepository>();
            services.AddScoped<ILogSuccessedLoginWriteRepository, LogSuccessedLoginWriteRepository>();
            #endregion


            #region Auth Repo
            services.AddScoped<IAuthPageReadRepository, AuthPageReadRepository>();
            services.AddScoped<IAuthPageWriteRepository, AuthPageWriteRepository>();

            services.AddScoped<IAuthRolePageReadRepository, AuthRolePageReadRepository>();
            services.AddScoped<IAuthRolePageWriteRepository, AuthRolePageWriteRepository>();

            services.AddScoped<IAuthRoleReadRepository, AuthRoleReadRepository>();
            services.AddScoped<IAuthRoleWriteRepository, AuthRoleWriteRepository>();

            services.AddScoped<IAuthUserRoleReadRepository, AuthUserRoleReadRepository>();
            services.AddScoped<IAuthUserRoleWriteRepository, AuthUserRoleWriteRepository>();

            #endregion


            #region Portal Parameter 
            services.AddScoped<IPortalParameterReadRepository, PortalParameterReadRepository>();
            services.AddScoped<IPortalParameterWriteRepository, PortalParameterWriteRepository>();
            #endregion


            #region Portal Text
            services.AddScoped<IPortalTextReadRepository, PortalTextReadRepository>();
            services.AddScoped<IPortalTextWriteRepository, PortalTextWriteRepository>();
            #endregion


            #region Marketing Customer
            services.AddScoped<IMarketingCustomerReadRepository, MarketingCustomerReadRepository>();
            services.AddScoped<IMarketingCustomerWriteRepository, MarketingCustomerWriteRepository>();
            #endregion


            #region Marketing Visit Plan
            services.AddScoped<IMarketingVisitPlanReadRepository, MarketingVisitPlanReadRepository>();
            services.AddScoped<IMarketingVisitPlanWriteRepository, MarketingVisitPlanWriteRepository>();
            #endregion



            #region Base
            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
            #endregion

            #region Log Email Send
            services.AddScoped<ILogEmailSendReadRepository, LogEmailSendReadRepository>();
            services.AddScoped<ILogEmailSendWriteRepository, LogEmailSendWriteRepository>();
            #endregion

            #region Personnel Address
            services.AddScoped<IPersonnelAddressReadRepository, PersonnelAddressReadRepository>();
            services.AddScoped<IPersonnelAddressWriteRepository, PersonnelAddressWriteRepository>();
            #endregion


            #region Personnel Working Table
            services.AddScoped<IPersonnelWorkingTableReadRepository, PersonnelWorkingTableReadRepository>();
            services.AddScoped<IPersonnelWorkingTableWriteRepository, PersonnelWorkingTableWriteRepository>();
            #endregion


            #region Personnel Permit Info
            services.AddScoped<IPersonnelPermitInfoReadRepository, PersonnelPermitInfoReadRepository>();
            services.AddScoped<IPersonnelPermitInfoWriteRepository, PersonnelPermitInfoWriteRepository>();
            #endregion


            #region Personnel Foreign Language
            services.AddScoped<IPersonnelForeignLanguageReadRepository, PersonnelForeignLanguageReadRepository>();
            services.AddScoped<IPersonnelForeignLanguageWriteRepository, PersonnelForeignLanguageWriteRepository>();
            #endregion


            #region Personnel Graduated School
            services.AddScoped<IPersonnelGraduatedSchoolReadRepository, PersonnelGraduatedSchoolReadRepository>();
            services.AddScoped<IPersonnelGraduatedSchoolWriteRepository, PersonnelGraduatedSchoolWriteRepository>();
            #endregion


            #region Personnel Passport Info
            services.AddScoped<IPersonnelPassportInfoReadRepository, PersonnelPassportInfoReadRepository>();
            services.AddScoped<IPersonnelPassportInfoWriteRepository, PersonnelPassportInfoWriteRepository>();
            #endregion


            #region Personnel Residence Info
            services.AddScoped<IPersonnelResidenceInfoReadRepository, PersonnelResidenceInfoReadRepository>();
            services.AddScoped<IPersonnelResidenceInfoWriteRepository, PersonnelResidenceInfoWriteRepository>();
            #endregion


            #region Personnel Document
            services.AddScoped<IPersonnelDocumentReadRepository, PersonnelDocumentReadRepository>();
            services.AddScoped<IPersonnelDocumentWriteRepository, PersonnelDocumentWriteRepository>();
            #endregion

            #region Permit Type
            services.AddScoped<IPermitTypeReadRepository, PermitTypeReadRepository>();
            services.AddScoped<IPermitTypeWriteRepository, PermitTypeWriteRepository>();
            #endregion




            #region City
            services.AddScoped<ICityReadRepository, CityReadRepository>();
            services.AddScoped<ICityWriteRepository, CityWriteRepository>();
            #endregion




            #region Department
            services.AddScoped<IDepartmentReadRepository, DepartmentReadRepository>();
            services.AddScoped<IDepartmentWriteRepository, DepartmentWriteRepository>();
            #endregion


            #region Department User
            services.AddScoped<IDepartmentUserReadRepository, DepartmentUserReadRepository>();
            services.AddScoped<IDepartmentUserWriteRepository, DepartmentUserWriteRepository>();
            #endregion

            #region Foreign Language
            services.AddScoped<IForeignLanguageReadRepository, ForeignLanguageReadRepository>();
            services.AddScoped<IForeignLanguageWriteRepository, ForeignLanguageWriteRepository>();
            #endregion


            #region Document Type
            services.AddScoped<IDocumentTypeReadRepository, DocumentTypeReadRepository>();
            services.AddScoped<IDocumentTypeWriteRepository, DocumentTypeWriteRepository>();
            #endregion


            #region Currency
            services.AddScoped<ICurrencyReadRepository, CurrencyReadRepository>();
            services.AddScoped<ICurrencyWriteRepository, CurrencyWriteRepository>();
            #endregion


            #region Job Type
            services.AddScoped<IJobTypeReadRepository, JobTypeReadRepository>();
            services.AddScoped<IJobTypeWriteRepository, JobTypeWriteRepository>();
            #endregion




            #region Oto Brand
            services.AddScoped<IOtoBrandReadRepository, OtoBrandReadRepository>();
            services.AddScoped<IOtoBrandWriteRepository, OtoBrandWriteRepository>();
            #endregion


            #region Room Type
            services.AddScoped<IRoomTypeReadRepository, RoomTypeReadRepository>();
            services.AddScoped<IRoomTypeWriteRepository, RoomTypeWriteRepository>();
            #endregion


            #region Measure Type
            services.AddScoped<IMeasureTypeReadRepository, MeasureTypeReadRepository>();
            services.AddScoped<IMeasureTypeWriteRepository, MeasureTypeWriteRepository>();
            #endregion
            #region Stock Card
            services.AddScoped<IStockCardReadRepository, StockCardReadRepository>();
            services.AddScoped<IStockCardWriteRepository, StockCardWriteRepository>();
            #endregion

            #region Stock Card Image
            services.AddScoped<IStockCardImageReadRepository, StockCardImageReadRepository>();
            services.AddScoped<IStockCardImageWriteRepository, StockCardImageWriteRepository>();
            #endregion


            #region Stock Movement
            services.AddScoped<IStockMovementReadRepository, StockMovementReadRepository>();
            services.AddScoped<IStockMovementWriteRepository, StockMovementWriteRepository>();
            #endregion

            #region Support Request
            services.AddScoped<ISupportRequestReadRepository, SupportRequestReadRepository>();
            services.AddScoped<ISupportRequestWriteRepository, SupportRequestWriteRepository>();
            #endregion


            #region Support Message
            services.AddScoped<ISupportMessageReadRepository, SupportMessageReadRepository>();
            services.AddScoped<ISupportMessageWriteRepository, SupportMessageWriteRepository>();
            #endregion


            #region Support Message Detail
            services.AddScoped<ISupportMessageDetailReadRepository, SupportMessageDetailReadRepository>();
            services.AddScoped<ISupportMessageDetailWriteRepository, SupportMessageDetailWriteRepository>();
            #endregion


            #region Task
            services.AddScoped<ITaskReadRepository, TaskReadRepository>();
            services.AddScoped<ITaskWriteRepository, TaskWriteRepository>();
            #endregion


            #region Task User
            services.AddScoped<ITaskUserReadRepository, TaskUserReadRepository>();
            services.AddScoped<ITaskUserWriteRepository, TaskUserWriteRepository>();
            #endregion


            #region Task Comment
            services.AddScoped<ITaskCommentReadRepository, TaskCommentReadRepository>();
            services.AddScoped<ITaskCommentWriteRepository, TaskCommentWriteRepository>();
            #endregion


            #region Task File
            services.AddScoped<ITaskFileReadRepository, TaskFileReadRepository>();
            services.AddScoped<ITaskFileWriteRepository, TaskFileWriteRepository>();
            #endregion

            #region Announcement
            services.AddScoped<IAnnouncementReadRepository, AnnouncementReadRepository>();
            services.AddScoped<IAnnouncementWriteRepository, AnnouncementWriteRepository>();
            #endregion


            #region Announcement Recipient
            services.AddScoped<IAnnouncementRecipientReadRepository, AnnouncementRecipientReadRepository>();
            services.AddScoped<IAnnouncementRecipientWriteRepository, AnnouncementRecipientWriteRepository>();
            #endregion

            #region Usershort cut
            services.AddScoped<IUserShortCutReadRepository, UserShortCutReadRepository>();
            services.AddScoped<IUserShortCutWriteRepository, UserShortCutWriteRepository>();
            #endregion

            services.AddScoped<IWarehouseReadRepository, WarehouseReadRepository>();
            services.AddScoped<IWarehouseWriteRepository, WarehouseWriteRepository>();
            services.AddScoped<ICountryReadRepository, CountryReadRepository>();
            services.AddScoped<ICountryWriteRepository, CountryWriteRepository>();

            #region SC Company
            services.AddScoped<ISCCompanyReadRepository, SCCompanyReadRepository>();
            services.AddScoped<ISCCompanyWriteRepository, SCCompanyWriteRepository>();
            #endregion


            #region SC Address
            services.AddScoped<ISCAddressReadRepository, SCAddressReadRepository>();
            services.AddScoped<ISCAddressWriteRepository, SCAddressWriteRepository>();
            #endregion


            #region SC Bank
            services.AddScoped<ISCBankReadRepository, SCBankReadRepository>();
            services.AddScoped<ISCBankWriteRepository, SCBankWriteRepository>();
            #endregion


            #region SC Employer
            services.AddScoped<ISCEmployerReadRepository, SCEmployerReadRepository>();
            services.AddScoped<ISCEmployerWriteRepository, SCEmployerWriteRepository>();
            #endregion


            #region SC Work History
            services.AddScoped<ISCWorkHistoryReadRepository, SCWorkHistoryReadRepository>();
            services.AddScoped<ISCWorkHistoryWriteRepository, SCWorkHistoryWriteRepository>();
            #endregion

            #region User Module Auth
            services.AddScoped<IUserModuleAuthReadRepository, UserModuleAuthReadRepository>();
            services.AddScoped<IUserModuleAuthWriteRepository, UserModuleAuthWriteRepository>();
            #endregion
            #region Offer
            services.AddScoped<IOfferReadRepository, OfferReadRepository>();
            services.AddScoped<IOfferWriteRepository, OfferWriteRepository>();
            #endregion


            #region Offer File
            services.AddScoped<IOfferFileReadRepository, OfferFileReadRepository>();
            services.AddScoped<IOfferFileWriteRepository, OfferFileWriteRepository>();
            #endregion


            #region Offer Transaction
            services.AddScoped<IOfferTransactionReadRepository, OfferTransactionReadRepository>();
            services.AddScoped<IOfferTransactionWriteRepository, OfferTransactionWriteRepository>();
            #endregion


            #region Organization Type
            services.AddScoped<IOrganizationTypeReadRepository, OrganizationTypeReadRepository>();
            services.AddScoped<IOrganizationTypeWriteRepository, OrganizationTypeWriteRepository>();
            #endregion


            #region Organization Item
            services.AddScoped<IOrganizationItemReadRepository, OrganizationItemReadRepository>();
            services.AddScoped<IOrganizationItemWriteRepository, OrganizationItemWriteRepository>();
            #endregion

            #region Finance Income Group
            services.AddScoped<IFinanceIncomeGroupReadRepository, FinanceIncomeGroupReadRepository>();
            services.AddScoped<IFinanceIncomeGroupWriteRepository, FinanceIncomeGroupWriteRepository>();
            #endregion


            #region Finance Income
            services.AddScoped<IFinanceIncomeReadRepository, FinanceIncomeReadRepository>();
            services.AddScoped<IFinanceIncomeWriteRepository, FinanceIncomeWriteRepository>();
            #endregion


            #region Finance Expense Group
            services.AddScoped<IFinanceExpenseGroupReadRepository, FinanceExpenseGroupReadRepository>();
            services.AddScoped<IFinanceExpenseGroupWriteRepository, FinanceExpenseGroupWriteRepository>();
            #endregion


            #region Finance Expense
            services.AddScoped<IFinanceExpenseReadRepository, FinanceExpenseReadRepository>();
            services.AddScoped<IFinanceExpenseWriteRepository, FinanceExpenseWriteRepository>();
            #endregion


            #region Finance Expense Detail
            services.AddScoped<IFinanceExpenseDetailReadRepository, FinanceExpenseDetailReadRepository>();
            services.AddScoped<IFinanceExpenseDetailWriteRepository, FinanceExpenseDetailWriteRepository>();
            #endregion

            #region Organization
            services.AddScoped<IOrganizationReadRepository, OrganizationReadRepository>();
            services.AddScoped<IOrganizationWriteRepository, OrganizationWriteRepository>();
            #endregion


            #region Organization Group
            services.AddScoped<IOrganizationGroupReadRepository, OrganizationGroupReadRepository>();
            services.AddScoped<IOrganizationGroupWriteRepository, OrganizationGroupWriteRepository>();
            #endregion

            #region Stock Category
            services.AddScoped<IStockCategoryReadRepository, StockCategoryReadRepository>();
            services.AddScoped<IStockCategoryWriteRepository, StockCategoryWriteRepository>();
            #endregion


            #region Organization File
            services.AddScoped<IOrganizationFileReadRepository, OrganizationFileReadRepository>();
            services.AddScoped<IOrganizationFileWriteRepository, OrganizationFileWriteRepository>();
            #endregion

            services.AddScoped<C.Context, Emasist2024Context>();
            services.AddScoped<IDatabaseService, DatabaseService>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

        }
    }
}
