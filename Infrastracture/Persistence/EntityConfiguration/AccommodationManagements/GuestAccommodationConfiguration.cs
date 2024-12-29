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
    public class GuestAccommodationConfiguration : IEntityTypeConfiguration<GuestAccommodation>
    {
        public void Configure(EntityTypeBuilder<GuestAccommodation> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.SCCompanyFK).WithMany(u => u.GuestAccommodations).HasForeignKey(y => y.GidHotelFK);
            builder.HasOne(y => y.BuyCurrencyFK).WithMany(u => u.BuyGuestAccommodations).HasForeignKey(y => y.GidBuyCurrencyFK);
            builder.HasOne(y => y.SellCurrencyFK).WithMany(u => u.SellGuestAccommodations).HasForeignKey(y => y.GidSellCurrencyFK);

            builder.Property(y => y.Title).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.Institution).IsRequired(false).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.GuestCount).IsRequired().HasColumnType("int");
            builder.Property(y => y.Description).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);
            builder.Property(y => y.BuyPrice).IsRequired(false).HasColumnType("float").HasMaxLength(10);
            builder.Property(y => y.SellPrice).IsRequired(false).HasColumnType("float").HasMaxLength(10);

            builder.HasMany(u => u.GuestAccommodationPersons).WithOne(y => y.GuestAccommodationFK).HasForeignKey(y => y.GidGuestAccommodationFK);
            builder.HasMany(u => u.GuestAccommodationRooms).WithOne(y => y.GuestAccommodationFK).HasForeignKey(y => y.GidGuestAccommodationFK);
        }
    }
}
