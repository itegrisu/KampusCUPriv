using Domain.Entities.SupplierCustomerManagements;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfiguration.SupplierManagements
{
    public class SCAddressConfiguration : IEntityTypeConfiguration<SCAddress>
    {
        public void Configure(EntityTypeBuilder<SCAddress> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.SCCompanyFK).WithMany(u => u.SCAddresses).HasForeignKey(y => y.GidSCCompanyFK);
            builder.HasOne(y => y.CityFK).WithMany(u => u.SCAddresses).HasForeignKey(y => y.GidCityFK);

            builder.Property(y => y.Title).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.District).IsRequired(false).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.PostalCode).IsRequired(false).HasColumnType("varchar").HasMaxLength(20);
            builder.Property(y => y.Address).IsRequired().HasColumnType("varchar").HasMaxLength(250);


        }
    }
}
