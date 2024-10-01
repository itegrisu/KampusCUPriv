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
    public class FinanceIncomeGroupConfiguration : IEntityTypeConfiguration<FinanceIncomeGroup>
    {
        public void Configure(EntityTypeBuilder<FinanceIncomeGroup> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");


            builder.Property(y => y.IncomeGroupName).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.RowNo).IsRequired().HasColumnType("int");

            builder.HasMany(u => u.FinanceIncomes).WithOne(y => y.FinanceIncomeGroupFK).HasForeignKey(y => y.GidIncomeGroupFK);

        }
    }
}
