using Domain.Entities.OrganizationManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrganizationManagement.Persistence.EntityConfiguration
{
    public class OrganizationItemMessageConfiguration : IEntityTypeConfiguration<OrganizationItemMessage>
    {
        public void Configure(EntityTypeBuilder<OrganizationItemMessage> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.OrganizationItemFK).WithMany(u => u.OrganizationItemMessages).HasForeignKey(y => y.GidOrganizationItemFK);
            builder.HasOne(y => y.UserFK).WithMany(u => u.OrganizationItemMessages).HasForeignKey(y => y.GidSendMessageUserFK);

            builder.Property(y => y.Message).IsRequired().HasColumnType("varchar").HasMaxLength(150);


        }
    }
}
