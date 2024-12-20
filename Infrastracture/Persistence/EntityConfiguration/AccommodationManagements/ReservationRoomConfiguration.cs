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
    public class ReservationRoomConfiguration : IEntityTypeConfiguration<ReservationRoom>
    {
        public void Configure(EntityTypeBuilder<ReservationRoom> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(t => t.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.ReservationDetailFK).WithMany(u => u.ReservationRooms).HasForeignKey(y => y.GidReservationDetailFK);

            builder.Property(y => y.RoomNo).IsRequired().HasColumnType("int");

            builder.HasMany(u => u.AccommodationDates).WithOne(y => y.ReservationRoomFK).HasForeignKey(y => y.GidRoomNoFK);
        }
    }
}
