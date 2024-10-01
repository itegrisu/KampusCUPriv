using Domain.Entities.DefinitionManagements;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfiguration.DefinitionManagements
{
    public class StockCategoryConfiguration : IEntityTypeConfiguration<StockCategory>
    {
        public void Configure(EntityTypeBuilder<StockCategory> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");


            builder.Property(y => y.Name).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.Code).IsRequired(false).HasColumnType("varchar").HasMaxLength(20);

            builder.HasMany(u => u.StockCards).WithOne(y => y.StockCategoryFK).HasForeignKey(y => y.GidStockCategoryFK);

        }
    }
}
