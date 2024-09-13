using Domain.Entities.PortalManagements;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfiguration.PortalManagements
{
    public class PortalTextConfiguration : IEntityTypeConfiguration<PortalText>
    {
        public void Configure(EntityTypeBuilder<PortalText> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.Property(y => y.Title).IsRequired().HasColumnType("varchar").HasMaxLength(255);
            builder.Property(y => y.Content).IsRequired(false).HasColumnType("varchar(max)");
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar(max)");
            builder.Property(y => y.IsRichTextBox).IsRequired().HasColumnType("bit");
            builder.Property(y => y.ContentRich).IsRequired(false).HasColumnType("varchar(max)");
        }
    }
}
