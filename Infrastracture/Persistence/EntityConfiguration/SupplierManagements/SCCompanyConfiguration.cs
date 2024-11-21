using Domain.Entities.SupplierCustomerManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.SupplierManagements
{
    public class SCCompanyConfiguration : IEntityTypeConfiguration<SCCompany>
    {
        public void Configure(EntityTypeBuilder<SCCompany> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");


            builder.Property(y => y.CompanyName).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.Phone).IsRequired(false).HasColumnType("varchar").HasMaxLength(20);
            builder.Property(y => y.WebSite).IsRequired(false).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.Email).IsRequired(false).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.Password).IsRequired(false).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.WebLoginStatus).IsRequired().HasColumnType("bit");
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);
            builder.Property(y => y.SpecialNote).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);
            builder.Property(y => y.TaxOffice).IsRequired(false).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.TaxNumber).IsRequired(false).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.Keywords).IsRequired(false).HasColumnType("varchar").HasMaxLength(300);
            builder.Property(y => y.SupplierRank).IsRequired(false).HasColumnType("int");
            builder.Property(y => y.CustomerRank).IsRequired(false).HasColumnType("int");
            builder.Property(y => y.IsHotel).IsRequired().HasColumnType("bit");

            builder.HasMany(u => u.SCAddresses).WithOne(y => y.SCCompanyFK).HasForeignKey(y => y.GidSCCompanyFK);
            builder.HasMany(u => u.SCBanks).WithOne(y => y.SCCompanyFK).HasForeignKey(y => y.GidSCCompanyFK);
            builder.HasMany(u => u.SCEmployers).WithOne(y => y.SCCompanyFK).HasForeignKey(y => y.GidSCCompanyFK);
            builder.HasMany(u => u.SCWorkHistories).WithOne(y => y.SCCompanyFK).HasForeignKey(y => y.GidSCCompanyFK);
            builder.HasMany(u => u.Organizations).WithOne(y => y.SCCompanyFK).HasForeignKey(y => y.GidCustomerFK);
            builder.HasMany(u => u.SCPersonnels).WithOne(y => y.SCCompanyFK).HasForeignKey(y => y.GidSCCompanyFK);
            builder.HasMany(u => u.VehicleTransactions).WithOne(y => y.SCCompanyFK).HasForeignKey(y => y.GidSupplierCustomerFK);
            builder.HasMany(u => u.FinanceBalances).WithOne(y => y.SCCompanyFK).HasForeignKey(y => y.GidSupplierCustomerFK);
            builder.HasMany(u => u.TransportationExternalServices).WithOne(y => y.SCCompanyFK).HasForeignKey(y => y.GidSupplierFK);
        }

    }
}
