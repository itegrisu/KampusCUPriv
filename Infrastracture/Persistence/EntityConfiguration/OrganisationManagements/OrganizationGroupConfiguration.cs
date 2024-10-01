using Domain.Entities.OrganizationManagements;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfiguration.OrganisationManagements
{
    public class OrganizationGroupConfiguration : IEntityTypeConfiguration<OrganizationGroup>
    {
        public void Configure(EntityTypeBuilder<OrganizationGroup> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.OrganizationFK).WithMany(u => u.OrganizationGroups).HasForeignKey(y => y.GidOrganizationFK);

            builder.Property(y => y.GroupName).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.RowNo).IsRequired().HasColumnType("int");

            builder.HasMany(u => u.OrganizationItems).WithOne(y => y.OrganizationGroupFK).HasForeignKey(y => y.GidOrganizationGroupFK);
        }
    }
}
