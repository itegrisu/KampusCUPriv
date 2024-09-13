using Domain.Entities.GeneralManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.GeneralManagements
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.CountryFK).WithMany(u => u.Users).HasForeignKey(y => y.GidUyrukFK);

            builder.Property(y => y.Adi).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.Soyadi).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.EPosta).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.Unvani).IsRequired(false).HasColumnType("varchar").HasMaxLength(60);
            builder.Property(y => y.Sifre).IsRequired().HasColumnType("varchar").HasMaxLength(255);
            builder.Property(y => y.SifreHash).IsRequired().HasColumnType("varchar").HasMaxLength(255);
            builder.Property(y => y.SifreGuncellemeToken).IsRequired().HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.TokenGecerlilikSuresi).IsRequired(false).HasColumnType("datetime");
            builder.Property(y => y.ProfilResmi).IsRequired(false).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.AktifHesapMi).IsRequired().HasColumnType("bit");
            builder.Property(y => y.SistemAdminMi).IsRequired().HasColumnType("bit");
            builder.Property(y => y.Gsm).IsRequired().HasColumnType("varchar").HasMaxLength(20);
            builder.Property(y => y.DogumYeri).IsRequired(false).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.DogumTarihi).IsRequired(false).HasColumnType("datetime");
            builder.Property(y => y.KimlikNo).IsRequired(false).HasColumnType("varchar").HasMaxLength(20);
            builder.Property(y => y.PasaportNo).IsRequired(false).HasColumnType("varchar").HasMaxLength(20);
            builder.Property(y => y.SGKNo).IsRequired(false).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.EhliyetNo).IsRequired(false).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.Not).IsRequired(false).HasColumnType("varchar").HasMaxLength(300);


            builder.HasMany(u => u.AsilYonetilenDepartmants).WithOne(y => y.AsilYoneticFK).HasForeignKey(y => y.GidAsilYoneticiFK);
            builder.HasMany(u => u.YedekYonetilenDepartmants).WithOne(y => y.YedekYoneticiFK).HasForeignKey(y => y.GidYedekYoneticiFK);
            builder.HasMany(u => u.DepartmentUsers).WithOne(y => y.UserFK).HasForeignKey(y => y.GidPersonelFK);
            builder.HasMany(u => u.PersonnelAddresses).WithOne(y => y.UserFK).HasForeignKey(y => y.GidPersonelFK);
            builder.HasMany(u => u.PersonnelWorkingTables).WithOne(y => y.UserFK).HasForeignKey(y => y.GidPersonelFK);
            builder.HasMany(u => u.PersonnelPermitInfos).WithOne(y => y.UserFK).HasForeignKey(y => y.GidPersonelFK);
            builder.HasMany(u => u.PersonnelForeignLanguages).WithOne(y => y.UserFK).HasForeignKey(y => y.GidPersonelFK);
            builder.HasMany(u => u.PersonnelGraduatedSchools).WithOne(y => y.UserFK).HasForeignKey(y => y.GidPersonelFK);
            builder.HasMany(u => u.PersonnelPassportInfos).WithOne(y => y.UserFK).HasForeignKey(y => y.GidPersonelFK);
            builder.HasMany(u => u.PersonnelResidenceInfos).WithOne(y => y.UserFK).HasForeignKey(y => y.GidPersonelFK);
            builder.HasMany(u => u.PersonnelDocuments).WithOne(y => y.UserFK).HasForeignKey(y => y.GidPersonelFK);

        }
    }
}
