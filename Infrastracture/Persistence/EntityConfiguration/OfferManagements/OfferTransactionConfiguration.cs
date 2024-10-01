using Domain.Entities.OfferManagements;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfiguration.OfferManagements
{
    public class OfferTransactionConfiguration : IEntityTypeConfiguration<OfferTransaction>
    {
        public void Configure(EntityTypeBuilder<OfferTransaction> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.OfferFK).WithMany(u => u.OfferTransactions).HasForeignKey(y => y.GidOfferFK);
            builder.HasOne(y => y.CurrencyFK).WithMany(u => u.OfferTransactions).HasForeignKey(y => y.GidCurrencyFK);

            builder.Property(y => y.OfferId).IsRequired().HasColumnType("varchar").HasMaxLength(20);
            builder.Property(y => y.Total).IsRequired().HasColumnType("float").HasMaxLength(10);
            builder.Property(y => y.OfferDeadline).IsRequired(false).HasColumnType("datetime");
            builder.Property(y => y.Document).IsRequired(false).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);


        }
    }
}
