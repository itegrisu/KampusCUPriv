using Domain.Entities.SupplierCustomerManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.SupplierManagements
{
    public class SCWorkHistoryConfiguration : IEntityTypeConfiguration<SCWorkHistory>
    {
        public void Configure(EntityTypeBuilder<SCWorkHistory> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.SCCompanyFK).WithMany(u => u.SCWorkHistories).HasForeignKey(y => y.GidSCCompanyFK);

            builder.Property(y => y.Title).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.Detail).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);
            builder.Property(y => y.WorkDate).IsRequired(false).HasColumnType("datetime");
            builder.Property(y => y.WorkFile).IsRequired(false).HasColumnType("varchar").HasMaxLength(150);

        }
    }
}
