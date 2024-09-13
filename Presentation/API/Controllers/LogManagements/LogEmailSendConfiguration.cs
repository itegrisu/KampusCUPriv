using Domain.Entities.LogManagements;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfiguration.LogManagements
{
    public class LogEmailSendConfiguration : IEntityTypeConfiguration<LogEmailSend>
    {
        public void Configure(EntityTypeBuilder<LogEmailSend> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.UserFK).WithMany(u => u.LogEmailSends).HasForeignKey(y => y.GidUserFK);

            builder.Property(y => y.Title).IsRequired().HasColumnType("varchar").HasMaxLength(200);
            builder.Property(y => y.Content).IsRequired().HasColumnType("varchar").HasMaxLength(1000);

        }
    }
}
