using Domain.Entities.MarketingManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.MarketingManagements
{
    public class MarketingVisitPlanConfiguration : IEntityTypeConfiguration<MarketingVisitPlan>
    {
        public void Configure(EntityTypeBuilder<MarketingVisitPlan> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.UserFK).WithMany(u => u.MarketingVisitPlans).HasForeignKey(y => y.GidPersonnelFK);
            builder.HasOne(y => y.MarketingCustomerFK).WithMany(u => u.MarketingVisitPlans).HasForeignKey(y => y.GidVisitCustomerFK);

            builder.Property(y => y.Title).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.PlanningVisitDate).IsRequired().HasColumnType("datetime");
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar").HasMaxLength(500);
            builder.Property(y => y.VisitDate).IsRequired(false).HasColumnType("datetime");
            builder.Property(y => y.VisitRank).IsRequired(false).HasColumnType("int");
            builder.Property(y => y.VisitNote).IsRequired(false).HasColumnType("varchar").HasMaxLength(300);
        }
    }
}
