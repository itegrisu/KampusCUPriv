using Domain.Entities.GeneralManagements;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.EntityConfiguration.GeneralManagements
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.DepartmentFK).WithMany(u => u.Users).HasForeignKey(y => y.GidDepartmentFK);
            builder.HasOne(y => y.ClassFK).WithMany(u => u.Users).HasForeignKey(y => y.GidClassFK);

            builder.Property(y => y.Name).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.LastName).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.Email).IsRequired().HasColumnType("varchar").HasMaxLength(250);
            builder.Property(y => y.Password).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.IsBloodDonor).IsRequired(false).HasColumnType("bit");

            builder.HasMany(u => u.Clubs).WithOne(y => y.UserFK).HasForeignKey(y => y.GidManagerFK);
            builder.HasMany(u => u.Announcements).WithOne(y => y.UserFK).HasForeignKey(y => y.GidUserFK);
        }
    }
}
