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


            builder.Property(y => y.UlkeAdi).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.UlkeKodu).IsRequired().HasColumnType("varchar").HasMaxLength(5);
            builder.Property(y => y.TelefonKodu).IsRequired(false).HasColumnType("varchar").HasMaxLength(5);
            builder.Property(y => y.RowNo).IsRequired().HasColumnType("int");

            builder.HasMany(u => u.Users).WithOne(y => y.CountryFK).HasForeignKey(y => y.GidUyrukFK);
            builder.HasMany(u => u.Cities).WithOne(y => y.CountryFK).HasForeignKey(y => y.GidUlkeFK);
        }
    }
}
