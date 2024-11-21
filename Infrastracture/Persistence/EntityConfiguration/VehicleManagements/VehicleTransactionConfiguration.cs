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
    public class VehicleTransactionConfiguration : IEntityTypeConfiguration<VehicleTransaction>
    {
        public void Configure(EntityTypeBuilder<VehicleTransaction> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.SCCompanyFK).WithMany(u => u.VehicleTransactions).HasForeignKey(y => y.GidSupplierCustomerFK);
            builder.HasOne(y => y.UserFK).WithMany(u => u.VehicleTransactions).HasForeignKey(y => y.GidVehicleUsePersonnelFK);
            builder.HasOne(y => y.VehicleAllFK).WithMany(u => u.VehicleTransactions).HasForeignKey(y => y.GidVehicleFK);

            builder.Property(y => y.StartKM).IsRequired().HasColumnType("int");
            builder.Property(y => y.EndKM).HasColumnType("int");
            builder.Property(y => y.Fee).IsRequired(false).HasColumnType("int");
            builder.Property(y => y.ContractStartDate).IsRequired(false).HasColumnType("datetime");
            builder.Property(y => y.ContractEndDate).IsRequired(false).HasColumnType("datetime");
            builder.Property(y => y.ContactPerson).IsRequired(false).HasColumnType("varchar").HasMaxLength(60);
            builder.Property(y => y.ContactPhone).IsRequired(false).HasColumnType("varchar").HasMaxLength(20);
            builder.Property(y => y.ArventoAPIInfo).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);
            builder.Property(y => y.LicenseFile).IsRequired(false).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.ContractFile).IsRequired(false).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);
            builder.Property(y => y.PurchaseDate).IsRequired(false).HasColumnType("datetime");
            builder.Property(y => y.EndDate).IsRequired(false).HasColumnType("datetime");

            builder.HasMany(u => u.FinanceBalances).WithOne(y => y.VehicleTransactionFK).HasForeignKey(y => y.GidVehicleTransactionFK);
        }
    }
}
