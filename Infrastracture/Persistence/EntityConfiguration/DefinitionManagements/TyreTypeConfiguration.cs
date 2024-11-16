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
    public class TyreTypeConfiguration : IEntityTypeConfiguration<TyreType>
    {
        public void Configure(EntityTypeBuilder<TyreType> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.Property(y => y.Title).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.Size).IsRequired(false).HasColumnType("varchar").HasMaxLength(50);

            builder.HasMany(u => u.Tyres).WithOne(y => y.TyreTypeFK).HasForeignKey(y => y.GidTyreTypeFK);
        }
    }
}
