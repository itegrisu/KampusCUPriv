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

            builder.Property(y => y.Name).IsRequired().HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(y => y.LastName).IsRequired().HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(y => y.Email).IsRequired().HasColumnType("nvarchar").HasMaxLength(250);
            builder.Property(y => y.Password).IsRequired().HasColumnType("nvarchar").HasMaxLength(500);
            builder.Property(y => y.PasswordSalt).IsRequired(false).HasColumnType("nvarchar").HasMaxLength(500);
            builder.Property(y => y.IsBloodDonor).IsRequired(false).HasColumnType("bit");
            builder.Property(y => y.IsEmailVerified).IsRequired(true).HasColumnType("bit");
            builder.Property(y => y.EmailVerificationCode).IsRequired(false).HasColumnType("varchar").HasMaxLength(6);
            builder.Property(y => y.EmailVerificationCodeExpire).IsRequired(false).HasColumnType("datetime");
            builder.Property(y => y.DeviceToken).IsRequired(false).HasColumnType("nvarchar").HasMaxLength(250);
            builder.Property(y => y.IsNotificationsEnabled).IsRequired().HasColumnType("bit");
            builder.Property(y => y.RefreshToken).IsRequired(false).HasColumnType("nvarchar").HasMaxLength(500);
            builder.Property(y => y.RefreshTokenExpiration).IsRequired(false).HasColumnType("datetime");

            builder.HasMany(u => u.Clubs).WithOne(y => y.UserFK).HasForeignKey(y => y.GidManagerFK);
            builder.HasMany(u => u.StudentAnnouncements).WithOne(y => y.UserFK).HasForeignKey(y => y.GidUserFK);
        }
    }
}
