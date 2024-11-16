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
    public class VehicleMaintenanceConfiguration : IEntityTypeConfiguration<VehicleMaintenance>
    {
        public void Configure(EntityTypeBuilder<VehicleMaintenance> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.VehicleAllFK).WithMany(u => u.VehicleMaintenances).HasForeignKey(y => y.GidVehicleFK);

            builder.Property(y => y.MaintenanceDate).IsRequired().HasColumnType("datetime");
            builder.Property(y => y.ResponsiblePerson).IsRequired(false).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.MaintenanceFee).IsRequired().HasColumnType("int");
            builder.Property(y => y.DocumentFile).IsRequired(false).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);
            builder.Property(y => y.MaintenanceScore).IsRequired(false).HasColumnType("int");
        }
    }
}
