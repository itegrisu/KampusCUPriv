using Domain.Entities.PortalManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfiguration.PortalManagements
{
    public class PortalParameterConfiguration : IEntityTypeConfiguration<PortalParameter>
    {
        public void Configure(EntityTypeBuilder<PortalParameter> builder)
    {
        builder.HasKey(t => t.Gid);
        builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

        builder.Property(y => y.Name).IsRequired().HasColumnType("varchar").HasMaxLength(250);
        builder.Property(y => y.StringValue).IsRequired(false).HasColumnType("varchar(max)");
        builder.Property(y => y.IntegerValue).IsRequired(false).HasColumnType("int");
        builder.Property(y => y.DecimalValue).IsRequired(false).HasColumnType("float");
        builder.Property(y => y.DateTimeValue).IsRequired(false).HasColumnType("datetime");
        builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar(max)");
    }
}
}
