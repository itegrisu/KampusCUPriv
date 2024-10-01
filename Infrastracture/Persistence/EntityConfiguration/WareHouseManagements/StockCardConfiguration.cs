using Domain.Entities.WarehouseManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.WareHouseManagements
{
    public class StockCardConfiguration : IEntityTypeConfiguration<StockCard>
    {
        public void Configure(EntityTypeBuilder<StockCard> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.StockCategoryFK).WithMany(u => u.StockCards).HasForeignKey(y => y.GidStockCategoryFK);
            builder.HasOne(y => y.MeasureTypeFK).WithMany(u => u.StockCards).HasForeignKey(y => y.GidMeasureFK);

            builder.Property(y => y.StockName).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.StockCode).IsRequired(false).HasColumnType("varchar").HasMaxLength(20);
            builder.Property(y => y.Brand).IsRequired(false).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);

            builder.HasMany(u => u.StockCardImages).WithOne(y => y.StockCardFK).HasForeignKey(y => y.GidStockCardFK);
            builder.HasMany(u => u.StockMovements).WithOne(y => y.StockCardFK).HasForeignKey(y => y.GidStockCardFK);

        }
    }
}
