using Domain.Entities.OrganizationManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrganizationManagement.Persistence.EntityConfiguration
{
    public class OrganizationItemFileConfiguration : IEntityTypeConfiguration<OrganizationItemFile>
    {
        public void Configure(EntityTypeBuilder<OrganizationItemFile> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.OrganizationItemFK).WithMany(u => u.OrganizationItemFiles).HasForeignKey(y => y.GidOrganizationItemFK);

            builder.Property(y => y.Title).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.Document).IsRequired(false).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar").HasMaxLength(300);
            builder.Property(y => y.RowNo).IsRequired().HasColumnType("int");

        }
    }
}
