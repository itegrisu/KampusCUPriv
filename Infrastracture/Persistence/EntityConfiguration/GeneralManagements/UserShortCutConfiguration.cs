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
    public class UserShortCutConfiguration : IEntityTypeConfiguration<UserShortCut>
    {
        public void Configure(EntityTypeBuilder<UserShortCut> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.UserFK).WithMany(u => u.UserShortCuts).HasForeignKey(y => y.GidUserFK);

            builder.Property(y => y.PageName).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.PageUrl).IsRequired().HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.RowNo).IsRequired().HasColumnType("int");


        }
    }
}
