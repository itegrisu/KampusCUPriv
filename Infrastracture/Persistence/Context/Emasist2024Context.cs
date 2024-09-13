using Core.Entities;
using Domain.Entities.AuthManagements;
using Domain.Entities.DefinitionManagements;
using Domain.Entities.GeneralManagements;
using Domain.Entities.LogManagements;
using Domain.Entities.PortalManagements;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityConfiguration.AuthManagements;
using Persistence.EntityConfiguration.DefinitionManagements;
using Persistence.EntityConfiguration.GeneralManagements;
using Persistence.EntityConfiguration.LogManagements;
using Persistence.EntityConfiguration.PortalManagements;
using C = Core.Context;

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
                relationship.DeleteBehavior = DeleteBehavior.Restrict;  //introducing hatas� i�in yaz�ld�
            }


            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRefreshTokenConfiguration());
            modelBuilder.ApplyConfiguration(new CountryConfiguration());

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

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //ChangeTracker : Entityler �zerinden yap�lan de�i�iklerin ya da yeni eklenen verinin yakalanmas�n� sa�layan propertydir. Update operasyonlar�nda Track edilen verileri yakalay�p elde etmemizi sa�lar.

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


        // DbSet, veritaban� tablosu �zerinde CRUD i�lemlerini ger�ekle�tirmeyi sa�lar
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
        public DbSet<PortalText> PortalTexts { get; set; }


    }
}
