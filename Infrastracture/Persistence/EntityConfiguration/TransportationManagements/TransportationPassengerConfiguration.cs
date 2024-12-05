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
    public class TransportationPassengerConfiguration : IEntityTypeConfiguration<TransportationPassenger>
    {
        public void Configure(EntityTypeBuilder<TransportationPassenger> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.TransportationGroupFK).WithMany(u => u.TransportationPassengers).HasForeignKey(y => y.GidTransportationGroupFK);

            builder.Property(y => y.Country).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.IdentityNo).IsRequired().HasColumnType("varchar").HasMaxLength(30);
            builder.Property(y => y.FirstName).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.LastName).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.Phone).IsRequired(false).HasColumnType("varchar").HasMaxLength(20); builder.Property(y => y.RefNoTransportationPassenger).IsRequired(false).HasColumnType("varchar").HasMaxLength(50);
        }
    }
}
