using Domain.Entities.DefinitionManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.DefinitionManagements
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");


            builder.Property(y => y.DovizAdi).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.DovizKodu).IsRequired(false).HasColumnType("varchar").HasMaxLength(20);
            builder.Property(y => y.DovizSimgesi).IsRequired(false).HasColumnType("varchar").HasMaxLength(5);

        }
    }
}
