using Domain.Entities.AccommodationManagements;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfiguration.AccommodationManagements
{
    public class GuestConfiguration : IEntityTypeConfiguration<Guest>
    {
        public void Configure(EntityTypeBuilder<Guest> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.CountryFK).WithMany(u => u.Guests).HasForeignKey(y => y.GidNationalityFK);

            builder.Property(y => y.IdNumber).IsRequired().HasColumnType("varchar").HasMaxLength(20);
            builder.Property(y => y.Name).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.Surename).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.Duty).IsRequired(false).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.Institution).IsRequired(false).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.Phone).IsRequired(false).HasColumnType("varchar").HasMaxLength(20);
            builder.Property(y => y.Email).IsRequired(false).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(y => y.HesCode).IsRequired(false).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);

            builder.HasMany(u => u.AccommodationDates).WithOne(y => y.GuestFK).HasForeignKey(y => y.GidGuestFK);
        }
    }
}
