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
    public class VehicleAccidentConfiguration : IEntityTypeConfiguration<VehicleAccident>
    {
        public void Configure(EntityTypeBuilder<VehicleAccident> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.VehicleAllFK).WithMany(u => u.VehicleAccidents).HasForeignKey(y => y.GidVehicleFK);

            builder.Property(y => y.AccidentDate).IsRequired().HasColumnType("datetime");
            builder.Property(y => y.Driver).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.AccidentFile).IsRequired(false).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.AccidentImageFile).IsRequired(false).HasColumnType("varchar").HasMaxLength(150);
        }
    }
}
