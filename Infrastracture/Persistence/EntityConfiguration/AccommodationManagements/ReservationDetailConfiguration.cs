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
    public class ReservationDetailConfiguration : IEntityTypeConfiguration<ReservationDetail>
    {
        public void Configure(EntityTypeBuilder<ReservationDetail> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.ReservationHotelFK).WithMany(u => u.ReservationDetails).HasForeignKey(y => y.GidReservationHotelFK);
            builder.HasOne(y => y.RoomTypeFK).WithMany(u => u.ReservationDetails).HasForeignKey(y => y.GidRoomTypeFK);

            builder.Property(y => y.ReservationDate).IsRequired().HasColumnType("datetime");
            builder.Property(y => y.RoomCount).IsRequired().HasColumnType("int");
            builder.Property(y => y.BuyPrice).IsRequired(false).HasColumnType("float").HasMaxLength(10);
            builder.Property(y => y.SellPrice).IsRequired(false).HasColumnType("float").HasMaxLength(10);

            builder.HasMany(u => u.ReservationRooms).WithOne(y => y.ReservationDetailFK).HasForeignKey(y => y.GidReservationDetailFK);
            builder.HasMany(u => u.AccommodationDates).WithOne(y => y.ReservationDetailFK).HasForeignKey(y => y.GidReservationDetailFK);
        }
    }
}
