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
    public class TransportationGroupConfiguration : IEntityTypeConfiguration<TransportationGroup>
    {
        public void Configure(EntityTypeBuilder<TransportationGroup> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.TransportationServiceFK).WithMany(u => u.TransportationGroups).HasForeignKey(y => y.GidTransportationServiceFK).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(y => y.StartCountryFK).WithMany(u => u.StartTransportationGroups).HasForeignKey(y => y.GidStartCountryFK).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(y => y.StartCityFK).WithMany(u => u.StartTransportationGroups).HasForeignKey(y => y.GidStartCityFK).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(y => y.StartDistrictFK).WithMany(u => u.StartTransportationGroups).HasForeignKey(y => y.GidStartDistrictFK).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(y => y.EndCountryFK).WithMany(u => u.EndTransportationGroups).HasForeignKey(y => y.GidEndCountryFK).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(y => y.EndCityFK).WithMany(u => u.EndTransportationGroups).HasForeignKey(y => y.GidEndCityFK).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(y => y.EndDistrictFK).WithMany(u => u.EndTransportationGroups).HasForeignKey(y => y.GidEndDistrictFK).OnDelete(DeleteBehavior.NoAction);



            builder.Property(y => y.GroupName).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.TransportationFee).IsRequired().HasColumnType("float").HasMaxLength(10);
            builder.Property(y => y.StartPlace).IsRequired().HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.EndPlace).IsRequired().HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);

            builder.HasMany(u => u.TransportationPassengers).WithOne(y => y.TransportationGroupFK).HasForeignKey(y => y.GidTransportationGroupFK);
        }
    }
}
