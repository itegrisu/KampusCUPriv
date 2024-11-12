using Domain.Entities.OrganizationManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration.OrganisationManagements
{
    public class OrganizationItemConfiguration : IEntityTypeConfiguration<OrganizationItem>
    {
        public void Configure(EntityTypeBuilder<OrganizationItem> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.OrganizationGroupFK).WithMany(u => u.OrganizationItems).HasForeignKey(y => y.GidOrganizationGroupFK);
            builder.HasOne(y => y.MainResponsibleUserFK).WithMany(u => u.OrganizationItems).HasForeignKey(y => y.GidMainResponsibleUserFK);

            builder.Property(y => y.ItemName).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.StartDate).IsRequired().HasColumnType("datetime");
            builder.Property(y => y.EndDate).IsRequired().HasColumnType("datetime");
            builder.Property(y => y.Priority).IsRequired().HasColumnType("bit");
            builder.Property(y => y.IsStar).IsRequired().HasColumnType("bit");
            builder.Property(y => y.RowNo).IsRequired().HasColumnType("int");

            builder.HasMany(u => u.OrganizationItemMessages).WithOne(y => y.OrganizationItemFK).HasForeignKey(y => y.GidOrganizationItemFK);
            builder.HasMany(u => u.OrganizationItemFiles).WithOne(y => y.OrganizationItemFK).HasForeignKey(y => y.GidOrganizationItemFK);

        }
    }
}
