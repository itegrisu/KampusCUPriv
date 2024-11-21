using Domain.Entities.OrganizationManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.OrganisationManagements
{
    public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.SCCompanyFK).WithMany(u => u.Organizations).HasForeignKey(y => y.GidCustomerFK);
            builder.HasOne(y => y.ResponsibleUserFK).WithMany(u => u.Organizations).HasForeignKey(y => y.GidResponsibleUserFK);
            builder.HasOne(y => y.OrganizationTypeFK).WithMany(u => u.Organizations).HasForeignKey(y => y.GidOrganizationTypeFK);

            builder.Property(y => y.OrganizationName).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.StartDate).IsRequired().HasColumnType("datetime");
            builder.Property(y => y.EndDate).IsRequired().HasColumnType("datetime");
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);

            builder.HasMany(u => u.Warehouses).WithOne(y => y.OrganizationFK).HasForeignKey(y => y.GidOrganizationFK);
            builder.HasMany(u => u.FinanceExpenses).WithOne(y => y.OrganizationFK).HasForeignKey(y => y.GidOrganizationFK);
            builder.HasMany(u => u.OrganizationGroups).WithOne(y => y.OrganizationFK).HasForeignKey(y => y.GidOrganizationFK);
            builder.HasMany(u => u.OrganizationFiles).WithOne(y => y.OrganizationFK).HasForeignKey(y => y.GidOrganizationFK); 
            builder.HasMany(u => u.Transportations).WithOne(y => y.OrganizationFK).HasForeignKey(y => y.GidOrganizationFK);
            builder.HasMany(u => u.TransportationExternalServices).WithOne(y => y.OrganizationFK).HasForeignKey(y => y.GidOrganizationFK);
        }
    }
}
