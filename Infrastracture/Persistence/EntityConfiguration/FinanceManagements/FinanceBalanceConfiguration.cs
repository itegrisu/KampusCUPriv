using Domain.Entities.FinanceManagements;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfiguration.FinanceManagements
{
    public class FinanceBalanceConfiguration : IEntityTypeConfiguration<FinanceBalance>
    {
        public void Configure(EntityTypeBuilder<FinanceBalance> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.SCCompanyFK).WithMany(u => u.FinanceBalances).HasForeignKey(y => y.GidSupplierCustomerFK);
            builder.HasOne(y => y.VehicleTransactionFK).WithMany(u => u.FinanceBalances).HasForeignKey(y => y.GidVehicleTransactionFK);
            builder.HasOne(y => y.TransportationFK).WithMany(u => u.FinanceBalances).HasForeignKey(y => y.GidTransportationFK);
            builder.HasOne(y => y.TransportationExternalServiceFK).WithMany(u => u.FinanceBalances).HasForeignKey(y => y.GidTransportationExternalServiceFK);
            builder.HasOne(y => y.CurrencyFK).WithMany(u => u.FinanceBalances).HasForeignKey(y => y.GidFeeCurrencyFK);

            builder.Property(y => y.ExpirationDate).IsRequired().HasColumnType("datetime");
            builder.Property(y => y.Fee).IsRequired().HasColumnType("float").HasMaxLength(10);
            builder.Property(y => y.PaymentDate).IsRequired(false).HasColumnType("datetime");
            builder.Property(y => y.PaymentFile).IsRequired(false).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);


        }
    }
}
