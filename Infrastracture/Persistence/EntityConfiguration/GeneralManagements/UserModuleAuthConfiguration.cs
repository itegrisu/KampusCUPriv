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
    public class UserModuleAuthConfiguration : IEntityTypeConfiguration<UserModuleAuth>
    {
        public void Configure(EntityTypeBuilder<UserModuleAuth> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.UserFK).WithMany(u => u.UserModuleAuths).HasForeignKey(y => y.GidUserFK);

        }
    }
}
