using Domain.Entities.SupportManagements;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfiguration.SupportManagements
{

    public class SupportMessageDetailConfiguration : IEntityTypeConfiguration<SupportMessageDetail>
    {
        public void Configure(EntityTypeBuilder<SupportMessageDetail> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

           
            builder.HasOne(detail => detail.SupportMessageFK)
               .WithMany(message => message.SupportMessageDetails)
               .HasForeignKey(detail => detail.GidMessageFK);

            builder.HasOne(y => y.UserFK).WithMany(u => u.SupportMessageDetails).HasForeignKey(y => y.GidReadUserFK);

            builder.Property(y => y.ReadDate).IsRequired().HasColumnType("datetime");
            builder.Property(y => y.ReadIp).IsRequired().HasColumnType("varchar").HasMaxLength(20);


        }
    }
}
