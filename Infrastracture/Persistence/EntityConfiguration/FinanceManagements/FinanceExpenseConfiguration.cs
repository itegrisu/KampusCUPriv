using Domain.Entities.FinanceManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.FinanceManagements
{
    public class FinanceExpenseConfiguration : IEntityTypeConfiguration<FinanceExpense>
    {
        public void Configure(EntityTypeBuilder<FinanceExpense> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.FinanceExpenseGroupFK).WithMany(u => u.FinanceExpenses).HasForeignKey(y => y.GidExpenseGroupFK);
            builder.HasOne(y => y.OrganizationFK).WithMany(u => u.FinanceExpenses).HasForeignKey(y => y.GidOrganizationFK);
            builder.HasOne(y => y.MoneySenderPersonnelFK).WithMany(u => u.SendedFinanceExpenses).HasForeignKey(y => y.GidMoneySenderPersonnelFK);
            builder.HasOne(y => y.MoneyReceivePersonnelFK).WithMany(u => u.ReceivedFinanceExpenses).HasForeignKey(y => y.GidMoneyReceivePersonnelFK);
            builder.HasOne(y => y.CurrencyFK).WithMany(u => u.FinanceExpenses).HasForeignKey(y => y.GidCurrencyFK);
            builder.HasOne(y => y.ApprovalReceiverFK).WithMany(u => u.ApprovedFinanceExpenses).HasForeignKey(y => y.GidApprovalReceiverFK);

            builder.Property(y => y.Title).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.AmountSpent).IsRequired().HasColumnType("float").HasMaxLength(10);
            builder.Property(y => y.TransactionDate).IsRequired().HasColumnType("datetime");
            builder.Property(y => y.Document).IsRequired(false).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);
            builder.Property(y => y.ReceiverAcceptDate).IsRequired(false).HasColumnType("datetime");
            builder.Property(y => y.ReceiverRejectDate).IsRequired(false).HasColumnType("datetime");
            builder.Property(y => y.ReceiverIpAddress).IsRequired(false).HasColumnType("varchar").HasMaxLength(20);

            builder.HasMany(u => u.FinanceExpenseDetails).WithOne(y => y.FinanceExpenseFK).HasForeignKey(y => y.GidExpenseFK);

        }
    }
}
