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
    public class TransportationExternalServiceConfiguration : IEntityTypeConfiguration<TransportationExternalService>
    {
        public void Configure(EntityTypeBuilder<TransportationExternalService> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.SCCompanyFK).WithMany(u => u.TransportationExternalServices).HasForeignKey(y => y.GidSupplierFK);
            builder.HasOne(y => y.OrganizationFK).WithMany(u => u.TransportationExternalServices).HasForeignKey(y => y.GidOrganizationFK);
            builder.HasOne(y => y.CurrencyFK).WithMany(u => u.TransportationExternalServices).HasForeignKey(y => y.GidFeeCurrencyFK);

            builder.Property(y => y.Title).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.Fee).IsRequired().HasColumnType("float").HasMaxLength(10);
            builder.Property(y => y.PlateNo).IsRequired().HasColumnType("varchar").HasMaxLength(20);
            builder.Property(y => y.PassengerCapacity).IsRequired(false).HasColumnType("int");
            builder.Property(y => y.VehicleOfficer).IsRequired(false).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.VehiclePhone).IsRequired(false).HasColumnType("varchar").HasMaxLength(20);
            builder.Property(y => y.IsHasFile).IsRequired(false).HasColumnType("bit");
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);

            builder.HasMany(u => u.FinanceBalances).WithOne(y => y.TransportationExternalServiceFK).HasForeignKey(y => y.GidTransportationExternalServiceFK);
        }
    }
}
