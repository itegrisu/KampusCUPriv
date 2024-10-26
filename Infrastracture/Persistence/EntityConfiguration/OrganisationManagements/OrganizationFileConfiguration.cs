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
    public class OrganizationFileConfiguration : IEntityTypeConfiguration<OrganizationFile>
    {
        public void Configure(EntityTypeBuilder<OrganizationFile> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.OrganizationFK).WithMany(u => u.OrganizationFiles).HasForeignKey(y => y.GidOrganizationFK);

            builder.Property(y => y.Title).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.Document).IsRequired(false).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar").HasMaxLength(300);
            builder.Property(y => y.RowNo).IsRequired().HasColumnType("int");
        }
    }
}
