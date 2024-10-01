using Domain.Entities.WarehouseManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.WareHouseManagements
{
    public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.OrganizationFK).WithMany(u => u.Warehouses).HasForeignKey(y => y.GidOrganizationFK);

            builder.Property(y => y.Name).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.Address).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);

            builder.HasMany(u => u.PreviousStockMovements).WithOne(y => y.PreviousWarehouseFK).HasForeignKey(y => y.GidPreviousWarehouseFK);
            builder.HasMany(u => u.NextStockMovements).WithOne(y => y.NextWarehouseFK).HasForeignKey(y => y.GidNextWarehouseFK);

        }
    }
}
