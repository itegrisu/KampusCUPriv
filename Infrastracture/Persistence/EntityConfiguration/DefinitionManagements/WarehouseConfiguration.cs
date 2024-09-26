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
    public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");


            builder.Property(y => y.WarehouseName).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.Location).IsRequired().HasColumnType("varchar").HasMaxLength(150);

            builder.HasMany(u => u.PreviousStockMovements).WithOne(y => y.PreviousWarehouseFK).HasForeignKey(y => y.GidPreviousWarehouseFK);
            builder.HasMany(u => u.NextStockMovements).WithOne(y => y.NextWarehouseFK).HasForeignKey(y => y.GidNextWarehouseFK);

        }
    }
}
