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

            builder.HasOne(y => y.ClubFK).WithMany(u => u.Admins).HasForeignKey(y => y.GidClubFK).IsRequired(true);
            builder.Property(y => y.Email).IsRequired().HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(y => y.Password).IsRequired().HasColumnType("nvarchar").HasMaxLength(500);
            builder.Property(y => y.PasswordSalt).IsRequired(false).HasColumnType("nvarchar").HasMaxLength(500);
            builder.Property(y => y.RefreshToken).IsRequired(false).HasColumnType("nvarchar").HasMaxLength(500);
            builder.Property(y => y.RefreshTokenExpiration).IsRequired(false).HasColumnType("datetime");
        }
    }
}
