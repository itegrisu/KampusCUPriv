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
    public class OfferConfiguration : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");


            builder.Property(y => y.Title).IsRequired().HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.Customer).IsRequired().HasColumnType("varchar").HasMaxLength(100);

            builder.HasMany(u => u.OfferFiles).WithOne(y => y.OfferFK).HasForeignKey(y => y.GidOfferFK);
            builder.HasMany(u => u.OfferTransactions).WithOne(y => y.OfferFK).HasForeignKey(y => y.GidOfferFK);

        }
    }
}
