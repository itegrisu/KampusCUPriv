using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.WarehouseManagements;

namespace Persistence.EntityConfiguration.WareHouseManagements
{
    public class StockMovementConfiguration : IEntityTypeConfiguration<StockMovement>
    {
        public void Configure(EntityTypeBuilder<StockMovement> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.StockCardFK).WithMany(u => u.StockMovements).HasForeignKey(y => y.GidStockCardFK);
            builder.HasOne(y => y.PreviousWarehouseFK).WithMany(u => u.PreviousStockMovements).HasForeignKey(y => y.GidPreviousWarehouseFK);
            builder.HasOne(y => y.NextWarehouseFK).WithMany(u => u.NextStockMovements).HasForeignKey(y => y.GidNextWarehouseFK);

            builder.Property(y => y.TransactionDate).IsRequired().HasColumnType("datetime");
            builder.Property(y => y.Amount).IsRequired().HasColumnType("int");
            builder.Property(y => y.Document).IsRequired(false).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar").HasMaxLength(500);


        }
    }
}
