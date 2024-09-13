using Domain.Entities.PersonnelManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.PersonnelManagements
{
    public class PersonnelAddressConfiguration : IEntityTypeConfiguration<PersonnelAddress>
    {
        public void Configure(EntityTypeBuilder<PersonnelAddress> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.UserFK).WithMany(u => u.PersonnelAddresses).HasForeignKey(y => y.GidPersonelFK);
            builder.HasOne(y => y.CityFK).WithMany(u => u.PersonnelAddresses).HasForeignKey(y => y.GidSehirFK);

            builder.Property(y => y.AdresBasligi).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.Adres).IsRequired().HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.Aciklama).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);
        }
    }
}
