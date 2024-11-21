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
    public class VehicleAllConfiguration : IEntityTypeConfiguration<VehicleAll>
    {
        public void Configure(EntityTypeBuilder<VehicleAll> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.OtoBrandFK).WithMany(u => u.VehicleAlls).HasForeignKey(y => y.GidVehicleBrand);

            builder.Property(y => y.PlateNumber).IsRequired().HasColumnType("varchar").HasMaxLength(15);
            builder.Property(y => y.Model).IsRequired(false).HasColumnType("varchar").HasMaxLength(60);
            builder.Property(y => y.Color).IsRequired(false).HasColumnType("varchar").HasMaxLength(30);
            builder.Property(y => y.EngineNo).IsRequired(false).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.ChassisNumber).IsRequired(false).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.PassengerCount).IsRequired().HasColumnType("int");
            builder.Property(y => y.IsSubmitted).IsRequired().HasColumnType("bit");
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);

            builder.HasMany(u => u.VehicleTransactions).WithOne(y => y.VehicleAllFK).HasForeignKey(y => y.GidVehicleFK);
            builder.HasMany(u => u.VehicleDocuments).WithOne(y => y.VehicleAllFK).HasForeignKey(y => y.GidVehicleFK);
            builder.HasMany(u => u.VehicleInsurances).WithOne(y => y.VehicleAllFK).HasForeignKey(y => y.GidVehicleFK);
            builder.HasMany(u => u.VehicleMaintenances).WithOne(y => y.VehicleAllFK).HasForeignKey(y => y.GidVehicleFK);
            builder.HasMany(u => u.VehicleInspections).WithOne(y => y.VehicleAllFK).HasForeignKey(y => y.GidVehicleFK);
            builder.HasMany(u => u.VehicleEquipments).WithOne(y => y.VehicleAllFK).HasForeignKey(y => y.GidVehicleFK);
            builder.HasMany(u => u.VehicleFuels).WithOne(y => y.VehicleAllFK).HasForeignKey(y => y.GidVehicleFK);
            builder.HasMany(u => u.VehicleTyreUses).WithOne(y => y.VehicleAllFK).HasForeignKey(y => y.GidVehicleFK);
            builder.HasMany(u => u.VehicleRequests).WithOne(y => y.VehicleAllFK).HasForeignKey(y => y.GidVehicleFK);
            builder.HasMany(u => u.TransportationServices).WithOne(y => y.VehicleAllFK).HasForeignKey(y => y.GidVehicleFK);
        }
    }
}
