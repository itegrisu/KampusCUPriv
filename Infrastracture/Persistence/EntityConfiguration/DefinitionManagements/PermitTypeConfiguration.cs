using Domain.Entities.DefinitionManagements;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfiguration.DefinitionManagements
{
    public class PermitTypeConfiguration : IEntityTypeConfiguration<PermitType>
    {
        public void Configure(EntityTypeBuilder<PermitType> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");


            builder.Property(y => y.IzinAdi).IsRequired().HasColumnType("varchar").HasMaxLength(100);

            builder.HasMany(u => u.PersonnelPermitInfos).WithOne(y => y.PermitTypeFK).HasForeignKey(y => y.GidPermitFK);
        }
    }
}
