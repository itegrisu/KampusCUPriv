using Domain.Entities.SupplierCustomerManagements;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfiguration.SupplierManagements
{
    public class SCEmployerConfiguration : IEntityTypeConfiguration<SCEmployer>
    {
        public void Configure(EntityTypeBuilder<SCEmployer> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.SCCompanyFK).WithMany(u => u.SCEmployers).HasForeignKey(y => y.GidSCCompanyFK);

            builder.Property(y => y.FullName).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.Duty).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.Phone).IsRequired(false).HasColumnType("varchar").HasMaxLength(20);
            builder.Property(y => y.Email).IsRequired(false).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.SpecialNote).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);


        }
    }
}
