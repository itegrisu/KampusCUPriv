using Domain.Entities.MarketingManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.MarketingManagements
{
    public class MarketingCustomerConfiguration : IEntityTypeConfiguration<MarketingCustomer>
    {
        public void Configure(EntityTypeBuilder<MarketingCustomer> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");


            builder.Property(y => y.FullName).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.Company).IsRequired(false).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.Duty).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.PreviousDuty).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);
            builder.Property(y => y.Gsm).IsRequired(false).HasColumnType("varchar").HasMaxLength(20);
            builder.Property(y => y.Email).IsRequired(false).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);

            builder.HasMany(u => u.MarketingVisitPlans).WithOne(y => y.MarketingCustomerFK).HasForeignKey(y => y.GidVisitCustomerFK);

        }
    }
}
