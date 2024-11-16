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
    public class VehicleTyreUseConfiguration : IEntityTypeConfiguration<VehicleTyreUse>
    {
        public void Configure(EntityTypeBuilder<VehicleTyreUse> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.VehicleAllFK).WithMany(u => u.VehicleTyreUses).HasForeignKey(y => y.GidVehicleFK);
            builder.HasOne(y => y.TyreFK).WithMany(u => u.VehicleTyreUses).HasForeignKey(y => y.GidTyreFK);

            builder.Property(y => y.InstallationDate).IsRequired().HasColumnType("datetime");
            builder.Property(y => y.TyreRemovalDate).IsRequired(false).HasColumnType("datetime");
        }
    }
}
