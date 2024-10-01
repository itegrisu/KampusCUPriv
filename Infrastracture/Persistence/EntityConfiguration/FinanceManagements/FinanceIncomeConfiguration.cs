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
    public class FinanceIncomeConfiguration : IEntityTypeConfiguration<FinanceIncome>
    {
        public void Configure(EntityTypeBuilder<FinanceIncome> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.FinanceIncomeGroupFK).WithMany(u => u.FinanceIncomes).HasForeignKey(y => y.GidIncomeGroupFK);
            builder.HasOne(y => y.CurrencyFK).WithMany(u => u.FinanceIncomes).HasForeignKey(y => y.GidCurrencyFK);

            builder.Property(y => y.Title).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.Fee).IsRequired().HasColumnType("float").HasMaxLength(10);
            builder.Property(y => y.MaturityDate).IsRequired().HasColumnType("datetime");
            builder.Property(y => y.Document).IsRequired(false).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);


        }
    }
}
