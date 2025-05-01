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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");


            builder.Property(y => y.Name).IsRequired().HasColumnType("nvarchar").HasMaxLength(50);

            builder.HasMany(u => u.Clubs).WithOne(y => y.CategoryFK).HasForeignKey(y => y.GidCategoryFK);
        }
    }
}
