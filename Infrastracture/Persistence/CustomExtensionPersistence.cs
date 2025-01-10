using Application.Abstractions.EntityServices;
using Application.Abstractions.UnitOfWork;
using Application.Repositories.ClubManagementRepos.ClubRepo;
using Application.Repositories.CommunicationManagementRepo.AnnouncementRepo;
using Application.Repositories.CommunicationManagementRepo.CalendarRepo;
using Application.Repositories.CommunicationManagementRepo.EventRepo;
using Application.Repositories.DefinitionManagementRepo.AnnouncementTypeRepo;
using Application.Repositories.DefinitionManagementRepo.CategoryRepo;
using Application.Repositories.DefinitionManagementRepo.ClassRepo;
using Application.Repositories.DefinitionManagementRepo.DepartmentRepo;
using Application.Repositories.GeneralManagementRepo.AdminRepo;
using Application.Repositories.GeneralManagementRepo.UserRepo;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Repositories.ClubManagements.ClubRepo;
using Persistence.Repositories.CommunicationManagements.AnnouncementRepo;
using Persistence.Repositories.CommunicationManagements.CalendarRepo;
using Persistence.Repositories.CommunicationManagements.EventRepo;
using Persistence.Repositories.DefinitionManagements.AnnouncementTypeRepo;
using Persistence.Repositories.DefinitionManagements.CategoryRepo;
using Persistence.Repositories.DefinitionManagements.ClassRepo;
using Persistence.Repositories.DefinitionManagements.DepartmentRepo;
using Persistence.Repositories.GeneralManagements.AdminRepo;
using Persistence.Repositories.GeneralManagements.UserRepo;
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

            #region Department
            services.AddScoped<IDepartmentReadRepository, DepartmentReadRepository>();
            services.AddScoped<IDepartmentWriteRepository, DepartmentWriteRepository>();
            #endregion

            #region Class
            services.AddScoped<IClassReadRepository, ClassReadRepository>();
            services.AddScoped<IClassWriteRepository, ClassWriteRepository>();
            #endregion

            #region Admin
            services.AddScoped<IAdminReadRepository, AdminReadRepository>();
            services.AddScoped<IAdminWriteRepository, AdminWriteRepository>();
            #endregion

            #region Category
            services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
            services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();
            #endregion

            #region Club
            services.AddScoped<IClubReadRepository, ClubReadRepository>();
            services.AddScoped<IClubWriteRepository, ClubWriteRepository>();
            #endregion

            #region Event
            services.AddScoped<IEventReadRepository, EventReadRepository>();
            services.AddScoped<IEventWriteRepository, EventWriteRepository>();
            #endregion

            #region Announcement
            services.AddScoped<IAnnouncementReadRepository, AnnouncementReadRepository>();
            services.AddScoped<IAnnouncementWriteRepository, AnnouncementWriteRepository>();
            #endregion

            #region Announcement Type
            services.AddScoped<IAnnouncementTypeReadRepository, AnnouncementTypeReadRepository>();
            services.AddScoped<IAnnouncementTypeWriteRepository, AnnouncementTypeWriteRepository>();
            #endregion

            #region Calendar
            services.AddScoped<ICalendarReadRepository, CalendarReadRepository>();
            services.AddScoped<ICalendarWriteRepository, CalendarWriteRepository>();
            #endregion

            services.AddScoped<C.Context, KampusCUContext>();
            services.AddScoped<IDatabaseService, DatabaseService>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
