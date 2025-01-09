using Domain.Entities.TransportationManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.TransportationManagements
{
    public class TransportationConfiguration : IEntityTypeConfiguration<Transportation>
    {
        public void Configure(EntityTypeBuilder<Transportation> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.OrganizationFK).WithMany(u => u.Transportations).HasForeignKey(y => y.GidOrganizationFK);
            builder.HasOne(y => y.FeeCurrencyFK).WithMany(u => u.Transportations).HasForeignKey(y => y.GidFeeCurrencyFK).IsRequired();

            //builder.Property(y => y.CustomerInfo).IsRequired().HasColumnType("varchar").HasMaxLength(150);
            builder.HasOne(y => y.SCCompanyFK).WithMany(u => u.Transportations).HasForeignKey(y => y.GidCustomerFK);
            builder.Property(y => y.TransportationNo).IsRequired().HasColumnType("varchar").HasMaxLength(20);
            builder.Property(y => y.Title).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.StartDate).IsRequired().HasColumnType("datetime");
            builder.Property(y => y.EndDate).IsRequired(false).HasColumnType("datetime");
            builder.Property(y => y.Fee).IsRequired().HasColumnType("float").HasMaxLength(10);

            builder.HasMany(u => u.TransportationServices).WithOne(y => y.TransportationFK).HasForeignKey(y => y.GidTransportationFK);
            builder.HasMany(u => u.FinanceBalances).WithOne(y => y.TransportationFK).HasForeignKey(y => y.GidTransportationFK);
        }
    }
}
