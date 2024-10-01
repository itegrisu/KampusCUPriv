using Domain.Entities.DefinitionManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.DefinitionManagements
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");


            builder.Property(y => y.Name).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.Code).IsRequired(false).HasColumnType("varchar").HasMaxLength(20);
            builder.Property(y => y.Symbol).IsRequired(false).HasColumnType("varchar").HasMaxLength(5);

            builder.HasMany(u => u.SCBanks).WithOne(y => y.CurrencyFK).HasForeignKey(y => y.GidCurrencyFK);
            builder.HasMany(u => u.FinanceIncomes).WithOne(y => y.CurrencyFK).HasForeignKey(y => y.GidCurrencyFK);
            builder.HasMany(u => u.FinanceExpenses).WithOne(y => y.CurrencyFK).HasForeignKey(y => y.GidCurrencyFK);
            builder.HasMany(u => u.FinanceExpenseDetails).WithOne(y => y.CurrencyFK).HasForeignKey(y => y.GidCurrencyFK);
            builder.HasMany(u => u.OfferTransactions).WithOne(y => y.CurrencyFK).HasForeignKey(y => y.GidCurrencyFK);

        }
    }
}
