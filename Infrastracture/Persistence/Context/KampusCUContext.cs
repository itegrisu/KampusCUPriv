using Core.Entities;
using Domain.Entities.ClubManagements;
using Domain.Entities.CommunicationManagements;
using Domain.Entities.DefinitionManagements;
using Domain.Entities.GeneralManagements;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityConfiguration.ClubManagements;
using Persistence.EntityConfiguration.CommunicationManagements;
using Persistence.EntityConfiguration.DefinitionManagements;
using Persistence.EntityConfiguration.GeneralManagements;
using C = Core.Context;

namespace Persistence.Context
{
    public class KampusCUContext : C.Context
    {


        public KampusCUContext(DbContextOptions<KampusCUContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;  //introducing hatas� i�in yaz�ld�
            }

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new ClassConfiguration());
            modelBuilder.ApplyConfiguration(new AdminConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ClubConfiguration());
            modelBuilder.ApplyConfiguration(new EventConfiguration());
            modelBuilder.ApplyConfiguration(new AnnouncementConfiguration());
            modelBuilder.ApplyConfiguration(new AnnouncementTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CalendarConfiguration());
            modelBuilder.ApplyConfiguration(new StudentClubConfiguration());


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
        public DbSet<Department> Departments { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<AnnouncementType> AnnouncementTypes { get; set; }
        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<StudentClub> StudentClubs { get; set; }
    }
}
