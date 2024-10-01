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
    public class FinanceExpenseGroupConfiguration : IEntityTypeConfiguration<FinanceExpenseGroup>
    {
        public void Configure(EntityTypeBuilder<FinanceExpenseGroup> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");


            builder.Property(y => y.Name).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.RowNo).IsRequired().HasColumnType("int");

            builder.HasMany(u => u.FinanceExpenses).WithOne(y => y.FinanceExpenseGroupFK).HasForeignKey(y => y.GidExpenseGroupFK);

        }
    }
}
