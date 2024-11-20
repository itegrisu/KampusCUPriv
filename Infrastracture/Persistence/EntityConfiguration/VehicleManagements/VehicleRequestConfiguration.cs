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
    public class VehicleRequestConfiguration : IEntityTypeConfiguration<VehicleRequest>
    {
        public void Configure(EntityTypeBuilder<VehicleRequest> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.VehicleAllFK).WithMany(u => u.VehicleRequests).HasForeignKey(y => y.GidVehicleFK);
            builder.HasOne(y => y.RequestUserFK).WithMany(u => u.VehicleUserRequest).HasForeignKey(y => y.GidRequestUserFK);
            builder.HasOne(y => y.ApprovedUserFK).WithMany(u => u.VehicleApprovedUserRequests).HasForeignKey(y => y.GidApprovedUserFK);

            builder.Property(y => y.StartDate).IsRequired().HasColumnType("datetime");
            builder.Property(y => y.EndDate).IsRequired().HasColumnType("datetime");
            builder.Property(y => y.UseAim).IsRequired().HasColumnType("varchar").HasMaxLength(250);


        }
    }
}
