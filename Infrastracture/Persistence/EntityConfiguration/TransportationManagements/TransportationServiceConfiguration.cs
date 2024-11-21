using Domain.Entities.TransportationManagements;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfiguration.TransportationManagements
{
    public class TransportationServiceConfiguration : IEntityTypeConfiguration<TransportationService>
    {
        public void Configure(EntityTypeBuilder<TransportationService> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.TransportationFK).WithMany(u => u.TransportationServices).HasForeignKey(y => y.GidTransportationFK);
            builder.HasOne(y => y.VehicleAllFK).WithMany(u => u.TransportationServices).HasForeignKey(y => y.GidVehicleFK);

            builder.Property(y => y.ServiceNo).IsRequired().HasColumnType("varchar").HasMaxLength(20);
            builder.Property(y => y.StartDate).IsRequired().HasColumnType("datetime");
            builder.Property(y => y.EndDate).IsRequired().HasColumnType("datetime");
            builder.Property(y => y.StartKM).IsRequired(false).HasColumnType("int");
            builder.Property(y => y.EndKM).IsRequired(false).HasColumnType("int");
            builder.Property(y => y.VehiclePhone).IsRequired(false).HasColumnType("varchar").HasMaxLength(20);
            builder.Property(y => y.TransportationFile).IsRequired(false).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);

            builder.HasMany(u => u.TransportationGroups).WithOne(y => y.TransportationServiceFK).HasForeignKey(y => y.GidTransportationServiceFK);
            builder.HasMany(u => u.TransportationPersonnels).WithOne(y => y.TransportationServiceFK).HasForeignKey(y => y.GidTransportationServiceFK);
        }
    }
}
