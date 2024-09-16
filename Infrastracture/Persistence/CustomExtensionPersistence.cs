using Application.Abstractions.EntityServices;
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
using Application.Repositories.DefinitionManagementRepos.OtoBrandRepo;
using Application.Repositories.DefinitionManagementRepos.PermitTypeRepo;
using Application.Repositories.DefinitionManagementRepos.RoomTypeRepo;
using Application.Repositories.GeneralManagementRepos.DepartmentRepo;
using Application.Repositories.GeneralManagementRepos.DepartmentUserRepo;
using Application.Repositories.GeneralManagementRepos.UserRefreshTokenRepo;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Application.Repositories.LogManagementRepos.LogAuthorizationErrorRepo;
using Application.Repositories.LogManagementRepos.LogEmailSendRepo;
using Application.Repositories.LogManagementRepos.LogFailedLoginRepo;
using Application.Repositories.LogManagementRepos.LogSuccessedLoginRepo;
using Application.Repositories.LogManagementRepos.LogUserPageVisitActionRepo;
using Application.Repositories.LogManagementRepos.LogUserPageVisitRepo;
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
using Core.Repositories.Abstracts;
using Core.Repositories.Concretes;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
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
using Persistence.Repositories.DefinitionManagementRepos.OtoBrandRepo;
using Persistence.Repositories.DefinitionManagementRepos.PermitTypeRepo;
using Persistence.Repositories.DefinitionManagementRepos.RoomTypeRepo;
using Persistence.Repositories.GeneralManagementRepos.DepartmentRepo;
using Persistence.Repositories.GeneralManagementRepos.DepartmentUserRepo;
using Persistence.Repositories.GeneralManagementRepos.UserRefreshTokenRepo;
using Persistence.Repositories.GeneralManagementRepos.UserRepo;
using Persistence.Repositories.LogManagementRepos.LogAuthorizationErrorRepo;
using Persistence.Repositories.LogManagementRepos.LogEmailSendRepo;
using Persistence.Repositories.LogManagementRepos.LogFailedLoginRepo;
using Persistence.Repositories.LogManagementRepos.LogSuccessedLoginRepo;
using Persistence.Repositories.LogManagementRepos.LogUserPageVisitActionRepo;
using Persistence.Repositories.LogManagementRepos.LogUserPageVisitRepo;
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
using Persistence.Services.EntityServices;
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

            #region Department
            services.AddScoped<IDepartmentReadRepository, DepartmentReadRepository>();
            services.AddScoped<IDepartmentWriteRepository, DepartmentWriteRepository>();
            #endregion


            #region Department User
            services.AddScoped<IDepartmentUserReadRepository, DepartmentUserReadRepository>();
            services.AddScoped<IDepartmentUserWriteRepository, DepartmentUserWriteRepository>();
            #endregion

            #region Country
            services.AddScoped<ICountryReadRepository, CountryReadRepository>();
            services.AddScoped<ICountryWriteRepository, CountryWriteRepository>();
            #endregion

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
            #region City
            services.AddScoped<ICityReadRepository, CityReadRepository>();
            services.AddScoped<ICityWriteRepository, CityWriteRepository>();
            #endregion


            #region Permit Type
            services.AddScoped<IPermitTypeReadRepository, PermitTypeReadRepository>();
            services.AddScoped<IPermitTypeWriteRepository, PermitTypeWriteRepository>();
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



            services.AddScoped<C.Context, Emasist2024Context>();
            services.AddScoped<IDatabaseService, DatabaseService>();
            services.AddScoped<ILogService, LogService>();


        }
    }
}
