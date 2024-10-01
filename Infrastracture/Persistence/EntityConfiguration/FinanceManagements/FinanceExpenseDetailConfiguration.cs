using Domain.Entities.FinanceManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.FinanceManagements
{
    public class FinanceExpenseDetailConfiguration : IEntityTypeConfiguration<FinanceExpenseDetail>
    {
        public void Configure(EntityTypeBuilder<FinanceExpenseDetail> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.FinanceExpenseFK).WithMany(u => u.FinanceExpenseDetails).HasForeignKey(y => y.GidExpenseFK);
            builder.HasOne(y => y.SpendPersonnelFK).WithMany(u => u.SpendedFinanceExpenseDetails).HasForeignKey(y => y.GidSpendPersonnelFK);
            builder.HasOne(y => y.CurrencyFK).WithMany(u => u.FinanceExpenseDetails).HasForeignKey(y => y.GidCurrencyFK);
            builder.HasOne(y => y.ControlPersonnelFK).WithMany(u => u.ControlledFinanceExpenseDetails).HasForeignKey(y => y.GidControlPersonnelFK);

            builder.Property(y => y.SpentTitle).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.Fee).IsRequired().HasColumnType("float").HasMaxLength(10);
            builder.Property(y => y.TransactionDate).IsRequired().HasColumnType("datetime");
            builder.Property(y => y.Document).IsRequired(false).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);
            builder.Property(y => y.ControlDate).IsRequired(false).HasColumnType("datetime");
            builder.Property(y => y.ControlDescription).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);


        }
    }
}
