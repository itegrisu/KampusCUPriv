using Domain.Entities.StockManagements;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfiguration.StockManagements
{
    public class StockCardImageConfiguration : IEntityTypeConfiguration<StockCardImage>
    {
        public void Configure(EntityTypeBuilder<StockCardImage> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.StockCardFK).WithMany(u => u.StockCardImages).HasForeignKey(y => y.GidStockCardFK);

            builder.Property(y => y.Title).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.Image).IsRequired(false).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.RowNo).IsRequired().HasColumnType("int");

        }
    }
}
