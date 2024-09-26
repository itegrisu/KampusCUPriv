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
    public class SupportRequestConfiguration : IEntityTypeConfiguration<SupportRequest>
    {
        public void Configure(EntityTypeBuilder<SupportRequest> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");


            builder.Property(y => y.Title).IsRequired().HasColumnType("varchar").HasMaxLength(100);

            builder.HasOne(y=>y.UserFK).WithMany(a=>a.SupportRequests).HasForeignKey(a=>a.CreatedUserFK).IsRequired();


            builder.HasMany(u => u.SupportMessages).WithOne(y => y.SupportRequestFK).HasForeignKey(y => y.GidSupportFK);

        }
    }
}
