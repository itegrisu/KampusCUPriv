using Domain.Entities.VehicleManagements;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfiguration.VehicleManagements
{
    public class TyreConfiguration : IEntityTypeConfiguration<Tyre>
    {
        public void Configure(EntityTypeBuilder<Tyre> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.TyreTypeFK).WithMany(u => u.Tyres).HasForeignKey(y => y.GidTyreTypeFK);

            builder.Property(y => y.TyreNo).IsRequired().HasColumnType("varchar").HasMaxLength(20);
            builder.Property(y => y.ProductionYear).IsRequired(false).HasColumnType("int");
            builder.Property(y => y.DateOfPurchase).IsRequired(false).HasColumnType("datetime");
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);

            builder.HasMany(u => u.VehicleTyreUses).WithOne(y => y.TyreFK).HasForeignKey(y => y.GidTyreFK);
        }
    }
}
