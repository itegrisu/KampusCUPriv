using Domain.Entities.ClubManagements;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfiguration.ClubManagements
{
    public class ClubConfiguration : IEntityTypeConfiguration<Club>
    {
        public void Configure(EntityTypeBuilder<Club> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.UserFK).WithMany(u => u.Clubs).HasForeignKey(y => y.GidManagerFK);
            builder.HasOne(y => y.CategoryFK).WithMany(u => u.Clubs).HasForeignKey(y => y.GidCategoryFK);

            builder.Property(y => y.Name).IsRequired().HasColumnType("nvarchar").HasMaxLength(100);
            builder.Property(y => y.Logo).IsRequired(false).HasColumnType("nvarchar").HasMaxLength(500);
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("nvarchar").HasMaxLength(250);
            builder.Property(y => y.Color).IsRequired(false).HasColumnType("varchar").HasMaxLength(8);

            builder.HasMany(u => u.Events).WithOne(y => y.ClubFK).HasForeignKey(y => y.GidClubFK);
            builder.HasMany(u => u.Announcements).WithOne(y => y.ClubFK).HasForeignKey(y => y.GidClubFK); builder.HasMany(u => u.StudentClubs).WithOne(y => y.ClubFK).HasForeignKey(y => y.GidClubFK);
            builder.HasMany(u => u.Admins).WithOne(y => y.ClubFK).HasForeignKey(y => y.GidClubFK);
        }
    }
}
