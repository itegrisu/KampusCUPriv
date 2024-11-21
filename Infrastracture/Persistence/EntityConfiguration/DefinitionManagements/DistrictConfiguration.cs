using Domain.Entities.DefinitionManagements;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfiguration.DefinitionManagements
{
    public class DistrictConfiguration : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.CityFK).WithMany(u => u.Districts).HasForeignKey(y => y.GidCityFK);

            builder.Property(y => y.DistrictCode).IsRequired().HasColumnType("int");
            builder.Property(y => y.DistrictName).IsRequired().HasColumnType("varchar").HasMaxLength(50);

            builder.HasMany(u => u.StartTransportationGroups).WithOne(y => y.StartDistrictFK).HasForeignKey(y => y.GidStartDistrictFK);
            builder.HasMany(u => u.EndTransportationGroups).WithOne(y => y.EndDistrictFK).HasForeignKey(y => y.GidEndDistrictFK);
        }
    }
}
