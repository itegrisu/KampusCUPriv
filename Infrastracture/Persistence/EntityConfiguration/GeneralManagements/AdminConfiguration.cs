using Domain.Entities.GeneralManagements;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfiguration.GeneralManagements
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.Property(y => y.Email).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.Password).IsRequired().HasColumnType("varchar").HasMaxLength(50);
        }
    }
}
