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
    public class SCBankConfiguration : IEntityTypeConfiguration<SCBank>
    {
        public void Configure(EntityTypeBuilder<SCBank> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.SCCompanyFK).WithMany(u => u.SCBanks).HasForeignKey(y => y.GidSCCompanyFK);
            builder.HasOne(y => y.CurrencyFK).WithMany(u => u.SCBanks).HasForeignKey(y => y.GidCurrencyFK);

            builder.Property(y => y.Bank).IsRequired().HasColumnType("varchar").HasMaxLength(60);
            builder.Property(y => y.BranchName).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.BranchCode).IsRequired(false).HasColumnType("varchar").HasMaxLength(20);
            builder.Property(y => y.AccountNumber).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.IbanNo).IsRequired(false).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.SwiftNo).IsRequired(false).HasColumnType("varchar").HasMaxLength(50);


        }
    }
}
