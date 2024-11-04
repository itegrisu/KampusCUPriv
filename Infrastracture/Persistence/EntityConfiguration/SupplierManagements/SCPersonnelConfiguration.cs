using Domain.Entities.SupplierCustomerManagements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfiguration.SupplierManagements
{
    public class SCPersonnelConfiguration : IEntityTypeConfiguration<SCPersonnel>
    {
        public void Configure(EntityTypeBuilder<SCPersonnel> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.SCCompanyFK).WithMany(u => u.SCPersonnels).HasForeignKey(y => y.GidSCCompanyFK);
            builder.HasOne(y => y.UserFK).WithMany(u => u.SCPersonnels).HasForeignKey(y => y.GidPersonnelFK);

        }
    }
}
