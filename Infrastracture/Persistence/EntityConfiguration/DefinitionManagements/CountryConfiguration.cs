using Domain.Entities.DefinitionManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.DefinitionManagements
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");


            builder.Property(y => y.Name).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.CountryCode).IsRequired().HasColumnType("varchar").HasMaxLength(5);
            builder.Property(y => y.PhoneCode).IsRequired(false).HasColumnType("varchar").HasMaxLength(5);
            builder.Property(y => y.RowNo).IsRequired().HasColumnType("int");

            builder.HasMany(u => u.Users).WithOne(y => y.CountryFK).HasForeignKey(y => y.GidNationalityFK);
            builder.HasMany(u => u.Cities).WithOne(y => y.CountryFK).HasForeignKey(y => y.GidCountryFK);
            builder.HasMany(u => u.StartTransportationGroups).WithOne(y => y.StartCountryFK).HasForeignKey(y => y.GidStartCountryFK);
            builder.HasMany(u => u.EndTransportationGroups).WithOne(y => y.EndCountryFK).HasForeignKey(y => y.GidEndCountryFK);
            builder.HasMany(u => u.Guests).WithOne(y => y.CountryFK).HasForeignKey(y => y.GidNationalityFK);
        }
    }
}
