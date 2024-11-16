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
    public class VehicleEquipmentConfiguration : IEntityTypeConfiguration<VehicleEquipment>
    {
        public void Configure(EntityTypeBuilder<VehicleEquipment> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.VehicleAllFK).WithMany(u => u.VehicleEquipments).HasForeignKey(y => y.GidVehicleFK);

            builder.Property(y => y.EquipmentName).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.DocumentFile).IsRequired(false).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);
        }
    }
}
