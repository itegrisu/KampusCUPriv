using Domain.Entities.StockManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.StockManagements
{
    public class StockCardConfiguration : IEntityTypeConfiguration<StockCard>
    {
        public void Configure(EntityTypeBuilder<StockCard> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            //builder.HasOne(y => y.CategoryFK).WithMany(u => u.StockCards).HasForeignKey(y => y.GidStockCategoryFK);
            //builder.HasOne(y => y.BrandFK).WithMany(u => u.StockCards).HasForeignKey(y => y.GidBrandFK);
            //builder.HasOne(y => y.UnitFK).WithMany(u => u.StockCards).HasForeignKey(y => y.GidUnitFK);
            //builder.HasOne(y => y.CurrencyFK).WithMany(u => u.StockCards).HasForeignKey(y => y.GidPriceCurrencyFK);

            builder.Property(y => y.StockCode).IsRequired(false).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.StockName).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.Price).IsRequired().HasColumnType("float");
            builder.Property(y => y.TaxRate).IsRequired().HasColumnType("int");
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar").HasMaxLength(1500);

            builder.HasMany(u => u.StockCardImages).WithOne(y => y.StockCardFK).HasForeignKey(y => y.GidStockCardFK);
            builder.HasMany(u => u.StockMovements).WithOne(y => y.StockCardFK).HasForeignKey(y => y.GidStockCardFK);

            //builder.HasMany(u => u.StoreSaleDetails).WithOne(y => y.StockCardFK).HasForeignKey(y => y.GidStockFK);
            //builder.HasMany(u => u.StoreAdvertisings).WithOne(y => y.StockCardFK).HasForeignKey(y => y.GidStockFK);

        }
    }
}
