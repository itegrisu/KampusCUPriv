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
    public class UserRefreshTokenConfiguration : IEntityTypeConfiguration<UserRefreshToken>
    {
        public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(y => y.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.Property(t => t.RefreshToken).IsRequired().HasMaxLength(255);
            builder.Property(t => t.Expiration).IsRequired();
            builder.HasOne(t => t.UserFK).WithMany(t => t.UserRefreshTokens).HasForeignKey(t => t.GidUserFK);


        }
    }
}
