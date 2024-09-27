using Domain.Entities.DefinitionManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.DefinitionManagements
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.CountryFK).WithMany(u => u.Cities).HasForeignKey(y => y.GidUlkeFK);

            builder.Property(y => y.SehirAdi).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.PlakaKodu).IsRequired(false).HasColumnType("varchar").HasMaxLength(5);

            builder.HasMany(u => u.PersonnelAddresses).WithOne(y => y.CityFK).HasForeignKey(y => y.GidSehirFK);
            builder.HasMany(u => u.SCAddresses).WithOne(y => y.CityFK).HasForeignKey(y => y.GidCityFK);

        }
    }
}
