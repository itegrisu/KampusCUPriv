using Domain.Entities.DefinitionManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.DefinitionManagements
{
    public class OrganizationTypeConfiguration : IEntityTypeConfiguration<OrganizationType>
    {
        public void Configure(EntityTypeBuilder<OrganizationType> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.Property(y => y.Name).IsRequired().HasColumnType("varchar").HasMaxLength(60);

            builder.HasMany(u => u.Organizations).WithOne(y => y.OrganizationTypeFK).HasForeignKey(y => y.GidOrganizationTypeFK);

        }
    }
}
